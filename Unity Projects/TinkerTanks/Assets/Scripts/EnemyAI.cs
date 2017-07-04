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
    Transform target;
    Transform lastKnownPosition;
    Vector3[] path;
    int targetIndex;
    Vector3 currentWayPoint;
    float distanceToEnemy = Mathf.Infinity;
    int patrolPointIndex = 0;
    float nodeDistanceThreshold = 5;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;        
    }

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
 
        //check distance to target
        if (target != null)
        {
            distanceToEnemy = Vector3.Distance(this.transform.position, target.transform.position);

            //check if player is within look radius 
            if (distanceToEnemy < lookRadius && CheckLineOfSight())
            {
                lastKnownPosition = target.transform;

                if (distanceToEnemy <= weaponRange)
                {
                    
                    currentState = AIState.Shoot;
                }
                
                currentState = AIState.Search;
            }            
        }

        if(lastKnownPosition == null)
        {
            currentState = AIState.Patrol;
        }

        //perfom task
        if (currentState == AIState.Patrol)
        {
            
            Patrolling();
        }       
        else if(currentState == AIState.Shoot)
        {
            
            FireTurret();
        }
        else if (currentState == AIState.Search)
        {
            
            Searching();
        }
    }
	
    //void OnCollisionEnter(Collision col)
    //{       

    //    if (col.gameObject.tag == "Bullet")
    //    {
    //        GameObject bullet = col.gameObject;
    //        int damage = bullet.GetComponent<BulletLogic>().GetDamage();

    //        TakeDamage(damage, bullet);
    //    }
    //}

    void Move()
    {        
        CalculateRotation();       

        // keep distance from target
        if (Vector3.Distance(this.transform.position, target.transform.position) > safeDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, moveSpeed * Time.deltaTime);
        }               
    }

    #region States

    void FireTurret()
    {
        //stop tank to shoot
        path = null;

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
            Debug.Log("patrolling path null");
            PathFinding(patrolNodes[patrolPointIndex].transform.position);
        }

        float distToNode = Vector3.Distance(this.transform.position, currentWayPoint);

        if (distToNode < nodeDistanceThreshold)
        {
            Debug.Log("at node");
            if (patrolPointIndex++ == patrolNodes.Length)
            {
                patrolPointIndex = 0;
            }            

            PathFinding(patrolNodes[patrolPointIndex].transform.position);
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
            Move();
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
        if (pathSuccessful)
        {
            path = newPath;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator UpdatePath()
    {
        if (Time.timeSinceLevelLoad < .3f)
        {
            yield return new WaitForSeconds(.3f);
        }
        PathRequestManager.RequestPath(new PathRequest(transform.position, lastKnownPosition.position, OnPathFound));

        float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
        Vector3 targetPosOld = lastKnownPosition.position;

        while (true)
        {
            yield return new WaitForSeconds(minPathUpdateTime);
            if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold)
            {
                PathRequestManager.RequestPath(new PathRequest(transform.position, lastKnownPosition.position, OnPathFound));
                targetPosOld = lastKnownPosition.position;
            }
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
            Vector3 dirTurret = target.transform.position - this.transform.position;
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
        this.gameObject.GetComponentInChildren<EnemyDeath>().Die();
        Destroy(this.gameObject);   
     
        // TODO: add effect plus maybe a pick up
        
    }

    //called when the player dies to remove it as the target
    public void RemoveTarget()
    {
        target = null;
    }
}
