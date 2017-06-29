using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float maxHealth;
    public float health;

	// Use this for initialization
	void Start () {

        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
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

        Destroy(this.gameObject);        
    }
}
