using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    
    public Transform partToRotate;
    public Transform firePoint;
    public GameObject bulletPreFab;

    [Header("Stats")]
    public float bulletCoolDown = 1f;
    public float turnSpeed = 10f;
    public float health = 100;
    public float moveSpeed = 10;
    public float lookRadius = 20;
    public float weaponRange = 20;

    //pathing
    Transform target;
    Vector3[] path;
    int targetIndex;
    Vector3 currentWayPoint;
    float distanceToEnemy = Mathf.Infinity;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        //PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
    }

    void Update()
    {
        if (target != null)
        {
            distanceToEnemy = Vector3.Distance(this.transform.position, target.transform.position);

            //check if player is within look radius 
            if (distanceToEnemy < lookRadius)
            {
                CheckLineOfSight();
            }

            if (bulletCoolDown < 1)
            {
                bulletCoolDown += Time.deltaTime;
            }            
        }       
    }
	
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("collision enemy");

        if (col.gameObject.tag == "Bullet")
        {
            GameObject bullet = col.gameObject;
            int damage = bullet.GetComponent<BulletLogic>().GetDamage();

            TakeDamage(damage, bullet);
        }
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        currentWayPoint = path[0];

        while (true)
        {
            if (transform.position == currentWayPoint)
            {
                targetIndex++;
                if (targetIndex >= path.Length)
                {
                    yield break;
                }

                currentWayPoint = path[targetIndex];
            }

            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    void CheckLineOfSight()
    {        
        CalculateRotation();

        RaycastHit los;
        if(Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out los, weaponRange))
        {
            if (los.transform.tag == "Player")
            {
                PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

                if (bulletCoolDown >= 1)
                {
                    //FireTurret();
                }
            }
        } 
    }

    void CalculateRotation()
    {

        //check input direction is not zero       

        if (currentWayPoint != null)
        {
            Vector3 dirTank = currentWayPoint - this.transform.position;
            Quaternion lookRotationTank = Quaternion.LookRotation(dirTank);
            Vector3 rotationTank = (Quaternion.Lerp(this.transform.rotation, lookRotationTank, Time.deltaTime * turnSpeed).eulerAngles);
            this.transform.rotation = Quaternion.Euler(0f, rotationTank.y, 0f);
        }


        Vector3 dirTurret = target.transform.position - this.transform.position;
        Quaternion lookRotationTurret = Quaternion.LookRotation(dirTurret);
        Vector3 rotationTurret = (Quaternion.Lerp(partToRotate.transform.rotation, lookRotationTurret, Time.deltaTime * turnSpeed).eulerAngles);
        partToRotate.transform.rotation = Quaternion.Euler(0f, rotationTurret.y, 0f);
        
    }

    void FireTurret()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        bulletCoolDown = 0;
    }

    void TakeDamage(int damage, GameObject bullet)
    {
        health -= damage;

        if (health <= 0)
        {
            Die(bullet);
        }
    }

    void Die(GameObject bullet)
    {
        Destroy(this.gameObject);
        Destroy(bullet);
    }

    //called when the player dies to remove it as the target
    public void RemoveTarget()
    {
        target = null;
    }
}
