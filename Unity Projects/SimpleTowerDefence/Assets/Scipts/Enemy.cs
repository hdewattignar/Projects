using UnityEngine;

public class Enemy : MonoBehaviour {

    public float startSpeed = 2.5f;

    [HideInInspector]
    public float speed;

    public float health = 100;
    public int value = 50;


    void Start()
    {
        speed = startSpeed;
    }

    public void Slow(float slowPercentage)
    {
        speed = startSpeed * (1f - slowPercentage);
    }

    public void TakeDamage(float dmg)
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
