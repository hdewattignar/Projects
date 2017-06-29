using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour {
    
    public GameObject impactEffect;
    public float bulletSpeed = 1;
    public int damage = 50;
    public float lifeSpan = 5;

    string origin;

    void Start()
    {        
        this.transform.parent = null;
    }

	// Update is called once per frame
	void Update () {

        //move bullet
        this.transform.Translate(0, 0, bulletSpeed * Time.deltaTime, Space.Self);

        //kill bullet after lifeSpan is up
        lifeSpan -= Time.deltaTime;

        if (lifeSpan < 0)
        {
            Destroy(gameObject);
        }

	}

    void OnCollisionEnter(Collision col)
    {
        impactEffect.GetComponent<Renderer>().material = col.gameObject.GetComponent<Renderer>().material;

        GameObject effectInst = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effectInst, 1f);

        if (col.gameObject.tag == "Enemy")
        {
            col.gameObject.GetComponent<EnemyAI>().TakeDamage(damage);
        }

        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerManager>().TakeDamage(damage);
        }

        Destroy(this.gameObject);
    }

    public int GetDamage()
    {
        return damage;
    }
}
