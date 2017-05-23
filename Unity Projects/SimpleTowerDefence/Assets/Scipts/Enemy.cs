using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed;
    public int health = 100;

    public int value = 50;

    private Transform target;
    private int wayPointIndex = 0;

    void Start()
    {
        target = Waypoints.points[0];
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.1f)
        {
            GetNextWaypoint();
        }
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

    public void TakeDamage(int dmg)
    {
        health -= dmg;

        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);

        //create effect

        //add money
        PlayerStats.Money += value;
    }
}
