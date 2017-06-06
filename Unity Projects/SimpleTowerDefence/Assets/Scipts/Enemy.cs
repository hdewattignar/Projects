using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float startSpeed = 2.5f;
    public float startHealth = 100;

    [HideInInspector]
    public float speed;

    private float health;
    public int value = 50;

    [Header("Unity Stuff")]
    public Image healthBar;

    private bool isDead = false;


    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void Slow(float slowPercentage)
    {
        speed = startSpeed * (1f - slowPercentage);
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;

        healthBar.fillAmount = health / startHealth;

        if (health <= 0f && isDead == false)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;        

        Destroy(gameObject);

        //create effect

        //update wave spawner
        WaveSpawner.EnemysAlive--;

        //add money
        PlayerStats.Money += value;
    }
}
