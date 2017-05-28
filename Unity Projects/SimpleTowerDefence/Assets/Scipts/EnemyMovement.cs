using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour {

    private Transform target;
    private int wayPointIndex = 0;

    private Enemy enemy;

	// Use this for initialization
	void Start () 
    {
        enemy = GetComponent<Enemy>();

        target = Waypoints.points[0];
	}

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }


    void GetNextWaypoint()
    {
        if (wayPointIndex >= Waypoints.points.Length - 1)
        {
            PathEnded();
            return;
        }


        wayPointIndex++;
        target = Waypoints.points[wayPointIndex];
    }

    void PathEnded()
    {
        PlayerStats.Lives--;
        Destroy(gameObject);
    }
}
