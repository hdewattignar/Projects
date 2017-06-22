using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    public float health = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("collision player");

        if (col.gameObject.tag == "Bullet")
        {
            GameObject bullet = col.gameObject;

            int damage = bullet.GetComponent<BulletLogic>().GetDamage();

            takeDamage(damage, bullet);
        }
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
        //remove the player as enemys target

        GameObject[] enemys = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemys.Length; i++)
        {
            enemys[i].GetComponent<EnemyAI>().RemoveTarget();
        }

            Destroy(this.gameObject);
        Destroy(bullet);
    }
}
