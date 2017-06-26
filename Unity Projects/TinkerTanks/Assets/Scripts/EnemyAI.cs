using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    
    public Transform partToRotate;
    public Transform firePoint;
    public GameObject bulletPreFab;

    public float bulletCoolDown = 1f;
    public float turnSpeed = 10f;
    public float health = 100;
    public float moveSpeed = 10;
    public float lookRadius = 20;
    public float weaponRange = 20;

    Transform target;
    float distanceToEnemy = Mathf.Infinity;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (target != null)
        {
            distanceToEnemy = Vector3.Distance(this.transform.position, target.transform.position);

            //check if player is within look radius 
            if (distanceToEnemy < lookRadius)
            {
                checkLineOfSight();
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

            takeDamage(damage, bullet);
        }
    }

    void checkLineOfSight()
    {        
        CalculateRotation();

        RaycastHit los;
        if(Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out los, weaponRange))
        {
            if (los.transform.tag == "Player")
            {
               

                if (bulletCoolDown >= 1)
                {
                    FireTurret();
                }
            }
        } 
    }

    void CalculateRotation()
    {

        //check input direction is not zero
        
        
        //Vector3 dir = tankInputDirection.transform.position - this.transform.position;
        //Quaternion lookRotation = Quaternion.LookRotation(dir);
        //Vector3 rotation = (Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles);
        //this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
       
        Vector3 dir = target.transform.position - this.transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = (Quaternion.Lerp(partToRotate.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles);
        partToRotate.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        
    }

    void FireTurret()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        bulletCoolDown = 0;
    }

    void takeDamage(int damage, GameObject bullet)
    {
        health -= damage;

        if (health <= 0)
        {
            die(bullet);
        }
    }

    void die(GameObject bullet)
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
