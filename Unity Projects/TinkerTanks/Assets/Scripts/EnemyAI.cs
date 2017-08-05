using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    //Ai states
    public enum AIState { Patrol, Shoot, Search, Chase };
    public AIState currentState = AIState.Patrol;

    [Header("GameObjects")]
    public Transform partToRotate;
    public Transform firePoint;
    public GameObject bulletPreFab;    
    public Transform lineOfSight;
    Transform[] patrolNodes;

    //public GameObject waypointMarker; //for testing
    
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
    Transform lastKnownPosition;
    Vector3[] path;
    int targetIndex;
    Vector3 currentWayPoint;
    float distanceToEnemy = Mathf.Infinity;
    int patrolNodeIndex = 0;
    public float nodeDistanceThreshold = 5;
    float updatePathTimer = 1f;

    [Header("Death")]
    public GameObject[] drops;
    public GameObject deathEffect;
    public int yOffset = 5;
    public int force = 2;

    void Awake()
    {
        getWaypoints();
    }

    void getWaypoints()
    {
        patrolNodes = FindObjectOfType<Manager>().GetWayPoints();
        if (patrolNodes.Length == 0)
            Debug.Log("nowaypoints");
    }
    void Update()
    {
        //if (patrolNodes == null)
        //{
        //    getWaypoints();
        //    Debug.Log("Getting Waypoints in update");
        //}

        UpdateCooldowns();

        if (currentWayPoint != null)
        {
            //waypointMarker.transform.position = currentWayPoint; // for testing 
        }

        CheckLineOfSight();

        //check distance to target
        if (lastKnownPosition != null)
        {
            distanceToEnemy = Vector3.Distance(this.transform.position, lastKnownPosition.position);

            //check if player is within look radius 
            if (distanceToEnemy <= lookRadius)
            {
                currentState = AIState.Search;

                if (distanceToEnemy <= weaponRange)
                {
                    FireTurret();
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
            
        }
        else if (currentState == AIState.Search)
        {
            Searching();
        }        
    }

    void UpdateCooldowns()
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

        if (updatePathTimer < 1)
        {
            updatePathTimer += Time.deltaTime;
            if (updatePathTimer > 1)
            {
                updatePathTimer = 1;
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

       
    }

    void Move()
    {        
        CalculateRotation();
        transform.position = Vector3.MoveTowards(transform.position, currentWayPoint, moveSpeed * Time.deltaTime);        
    }

    #region States

    void FireTurret()
    {      
        
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
            SortWayPoints();
            PathFinding(patrolNodes[0].transform.position);
        }

        float distToNode = Vector3.Distance(transform.position, patrolNodes[patrolNodeIndex].transform.position);

        if (distToNode <= nodeDistanceThreshold)
        {            
            patrolNodeIndex++;            

            if (patrolNodeIndex >= patrolNodes.Length)
            {                
                patrolNodeIndex = 0;
            }            

            PathFinding(patrolNodes[patrolNodeIndex].transform.position);            
        }       

        Move();
    }

    void PathFinding(Vector3 pathTo)
    {
        PathRequestManager.RequestPath(new PathRequest(transform.position, pathTo, OnPathFound));        
    }

    void Searching()
    {
        if (updatePathTimer == 1)
        {
            updatePathTimer = 0;
            PathFinding(lastKnownPosition.position);
        }

        if(lastKnownPosition != null && Vector3.Distance(transform.position, lastKnownPosition.position) > nodeDistanceThreshold)
        {            
            // keep distance from target
            if (Vector3.Distance(this.transform.position, lastKnownPosition.position) > safeDistance)
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

    void showPathNodes()
    {
        if (path != null)
        {
            foreach (Vector3 p in path)
            {
                //GameObject t = Instantiate(waypointMarker, p, this.transform.rotation);
                //Destroy(t, 1f);
            }
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
            //showPathNodes();
        }
    }    

    IEnumerator FollowPath()
    {        
        currentWayPoint = path[0];
        targetIndex = 0;

        while (this.gameObject != null)
        {
            if (path == null)
                Debug.Log("path null");            

            float distToNode = Vector3.Distance(transform.position, currentWayPoint);
            
            if (distToNode < 2)
            {                
                targetIndex++;
                if (targetIndex >= path.Length)
                {                    
                    yield break;
                }
                
                currentWayPoint = path[targetIndex];                
            }

            //showPathNodes();

            Move();
            yield return null; 
        }              
    }
    #endregion

    
    bool CheckLineOfSight()
    {
        Collider[] hitColliders = Physics.OverlapSphere(lineOfSight.transform.position, lookRadius);
        RaycastHit los;

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].tag != "Enemy")
            {
                Vector3 dir = hitColliders[i].transform.position - lineOfSight.transform.position;
                Quaternion lookRotation = Quaternion.LookRotation(dir);
                Vector3 rotation = (Quaternion.Lerp(lineOfSight.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles);
                lineOfSight.transform.rotation = Quaternion.Euler(rotation.x, rotation.y, 0f);                

                if (Physics.Raycast(lineOfSight.transform.position, lineOfSight.transform.forward, out los, lookRadius))
                {
                    if (los.transform.tag == "Player")
                    {
                        //Debug.DrawRay(lineOfSight.transform.position, lineOfSight.transform.forward, Color.red, 1f);
                        lastKnownPosition = los.transform;
                        return true;
                    }
                }
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

        FindObjectOfType<Spawner>().EnemyDied();

        Destroy(this.gameObject);
    }

    void SortWayPoints()
    {        
        int n = patrolNodes.Length;
        
        bool sorted = false;

        while(true)
        {
            bool swap = false;

            for (int i = 1; i < n; i++)
            {
                if ((Vector3.Distance(this.transform.position, patrolNodes[i - 1].position)) > (Vector3.Distance(this.transform.position, patrolNodes[i].position)))
                {
                    SwapPatrolNodes(i - 1, i);
                    swap = true;
                }                
            }

            if (swap == false)
            {
                break;
            }
        }
    }

    void SwapPatrolNodes(int a, int b)
    {        
        Transform temp;
        temp = patrolNodes[a];
        patrolNodes[a] = patrolNodes[b];
        patrolNodes[b] = temp;
    }

    //called when the player dies to remove it as the target
    public void RemoveTarget()
    {
        lastKnownPosition = null;
    }
}
