using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public int health = 100;

	
    void OnCollisionEnter(Collision col)
    {
        Debug.Log("collision");

        if (col.gameObject.tag == "Bullet")
        {
            GameObject bullet = col.gameObject;
            int damage = bullet.GetComponent<BulletLogic>().GetDamage();

            doDamage(damage, bullet);
        }
    }

    void doDamage(int damage, GameObject bullet)
    {
        health -= damage;

        if (health <= 0)
        {
            die(bullet);
        }
    }

    void die(GameObject bullet)
    {
        Destroy(this.gameObject);
        Destroy(bullet);
    }
}
