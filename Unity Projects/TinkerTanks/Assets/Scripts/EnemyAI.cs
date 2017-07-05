using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    //Ai states
    public enum AIState { Patrol, Shoot, Search };
    public AIState currentState = AIState.Patrol;

    [Header("GameObjects")]
    public Transform partToRotate;
    public Transform firePoint;
    public GameObject bulletPreFab;
    public Transform[] patrolNodes;
    
    const float minPathUpdateTime = .2f;
    const float pathUpdateMoveThreshold = .5f;

    [Header("Stats")]
    public float bulletCoolDown = 1f;
    public float waitAfterFire = 1f;
    public float turnSpeed = 10f;
    public float health = 100;
    public float moveSpeed = 10;
    public float lookRadius = 20;
    public float weaponRange = 20;
    public float safeDistance = 15;

    //pathing  
    [Header("pathing")]
    public Transform lastKnownPosition;
    Vector3[] path;
    int targetIndex;
    public Vector3 currentWayPoint;
    public float distanceToEnemy = Mathf.Infinity;
    public int patrolNodeIndex = 0;
    float nodeDistanceThreshold = 5;

    [Header("Death")]
    public GameObject[] drops;
    public GameObject deathEffect;
    public int yOffset = 5;
    public int force = 2;
   

    void Update()
    {

        
        //cooldown weapon
        if (bulletCoolDown < 1)
        {
            bulletCoolDown += Time.deltaTime;
            if (bulletCoolDown > 1)
            {
                bulletCoolDown = 1;
            }
        }

        //wait between shots
        if (waitAfterFire < 1)
        {
            waitAfterFire += Time.deltaTime;
            if (waitAfterFire > 1)
            {
                waitAfterFire = 1;
            }
        }

        CheckLineOfSight();

        //check distance to target
        if (lastKnownPosition != null)
        {
            distanceToEnemy = Vector3.Distance(this.transform.position, lastKnownPosition.transform.position);

            //check if player is within look radius 
            if (distanceToEnemy <= lookRadius)
            {
                currentState = AIState.Search;

                if (distanceToEnemy <= weaponRange)
                {
                    currentState = AIState.Shoot;
                }
            }
        }

        if (lastKnownPosition == null)
        {
            currentState = AIState.Patrol;
        }

        //perfom task
        if (currentState == AIState.Patrol)
        {
            Patrolling();
        }
        else if (currentState == AIState.Shoot)
        {
            FireTurret();
        }
        else if (currentState == AIState.Search)
        {

            Searching();
        }        
    }    

    void Move()
    {        
        CalculateRotation();
        transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, moveSpeed * Time.deltaTime);           
    }

    #region States

    void FireTurret()
    {
        //stop tank to shoot
        path = null;
        CalculateRotation();

        if (bulletCoolDown >= 1)
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
            bulletCoolDown = 0;
            waitAfterFire = 0;
        }
    }

    void Patrolling()
    {
        if (path == null)
        {            
            PathFinding(patrolNodes[patrolNodeIndex].transform.position);
        }

        float distToNode = Vector3.Distance(this.transform.position, patrolNodes[patrolNodeIndex].transform.position);

        if (distToNode < nodeDistanceThreshold)
        {
            patrolNodeIndex++;

            if (patrolNodeIndex == patrolNodes.Length)
            {
                patrolNodeIndex = 0;
            }            

            PathFinding(patrolNodes[patrolNodeIndex].transform.position);
            Debug.Log(patrolNodes[patrolNodeIndex].transform.position);
        }

        Move();
    }

    void PathFinding(Vector3 pathTo)
    {
        PathRequestManager.RequestPath(new PathRequest(transform.position, pathTo, OnPathFound));        
    }

    void Searching()
    {
        if(lastKnownPosition != null && Vector3.Distance(transform.position, lastKnownPosition.position) > nodeDistanceThreshold)
        {
            PathFinding(lastKnownPosition.position);
            // keep distance from target
            if (Vector3.Distance(this.transform.position, lastKnownPosition.transform.position) > safeDistance)
            {
                Move();
            }            
        }
        else
        {
            path = null;
            currentState = AIState.Patrol;
        }        
    }

    #endregion

    #region pathfinding
    //path finding
    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful  && this.gameObject != null)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }    

    IEnumerator FollowPath()
    {        
        currentWayPoint = path[0];

        while (this.gameObject != null)
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

            Move();
            yield return null; 
        }              
    }
    #endregion

    
    bool CheckLineOfSight()
    {
        RaycastHit los;
        if(Physics.Raycast(firePoint.transform.position, firePoint.transform.forward, out los, weaponRange))
        {
            if (los.transform.tag == "Player")
            {
                lastKnownPosition = los.transform;
                return true;                
            }           
        }

        return false;
    }

    void CalculateRotation()
    {
        if (currentWayPoint != null)
        {
            Vector3 dirTank = currentWayPoint - this.transform.position;
            Quaternion lookRotationTank = Quaternion.LookRotation(dirTank);
            Vector3 rotationTank = (Quaternion.Lerp(this.transform.rotation, lookRotationTank, Time.deltaTime * turnSpeed).eulerAngles);
            this.transform.rotation = Quaternion.Euler(0f, rotationTank.y, 0f);
        }

        if (distanceToEnemy < lookRadius)
        {
            Vector3 dirTurret = lastKnownPosition.transform.position - this.transform.position;
            Quaternion lookRotationTurret = Quaternion.LookRotation(dirTurret);
            Vector3 rotationTurret = (Quaternion.Lerp(partToRotate.transform.rotation, lookRotationTurret, Time.deltaTime * turnSpeed).eulerAngles);
            partToRotate.transform.rotation = Quaternion.Euler(0f, rotationTurret.y, 0f);   
        }
        else
        {
            partToRotate.rotation = this.transform.rotation;
        }     
    }    

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        StopCoroutine("FollowPath");        

        deathEffect.GetComponent<Renderer>().material = this.gameObject.GetComponent<Renderer>().material;
        GameObject effectInst = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effectInst, 1f);

        Vector3 dropPositionOffset = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);

        for (int i = 0; i < drops.Length; i++)
        {
            GameObject currentDrop = (GameObject)Instantiate(drops[i], dropPositionOffset, this.transform.rotation);
            Rigidbody rb = currentDrop.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(Random.Range(-force, force), force, Random.Range(-force, force)));
        }

        Destroy(this.gameObject);
    }

    //called when the player dies to remove it as the target
    public void RemoveTarget()
    {
        lastKnownPosition = null;
    }
}
