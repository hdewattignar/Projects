using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour {

    public float bulletSpeed = 1;
    public int damage = 50;
    public float lifeSpan = 5;

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

    public int GetDamage()
    {
        return damage;
    }
}
