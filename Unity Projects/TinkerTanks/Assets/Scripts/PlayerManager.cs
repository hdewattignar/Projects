using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    
    
    public Transform firePoint;
    public GameObject bulletPreFab;

    public float bulletCoolDown = 1;
    public float maxHealth;
    public float health;

	// Use this for initialization
	void Start () {

        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

        if (bulletCoolDown < 1)
        {
            bulletCoolDown += Time.deltaTime;

            if (bulletCoolDown > 1)
            {
                bulletCoolDown = 1;
            }
        }
		
	}

    public void FireTurret()
    {
        if (bulletCoolDown == 1)
        {
            GameObject bulletGO = (GameObject)Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
            bulletCoolDown = 0;
        }        
    }

    public void Heal(float healthRegen)
    {
        health += healthRegen;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            die();
        }
    }

    void die()
    {
        //remove the player as enemys target

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<EnemyAI>().RemoveTarget();
        }

        Camera.main.transform.parent = null;

        Destroy(this.gameObject);        
    }

    public float GetHealthPertentage()
    {
        if (health > 0)        
            return health / maxHealth;        
        else
            return 0;
    }

    public float GetCoolDownPercentage()
    {
        if (bulletCoolDown > 0)
            return bulletCoolDown / 1;
        else
            return 0;
    }
}
