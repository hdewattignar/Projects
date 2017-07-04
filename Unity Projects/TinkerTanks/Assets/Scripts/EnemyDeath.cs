using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour {


    public GameObject[] drops;
    public GameObject deathEffect;
    public int yOffset = 5;
    public int force = 2;

    public void Die()
    {
        deathEffect.GetComponent<Renderer>().material = this.gameObject.GetComponentInParent<Renderer>().material;
        GameObject effectInst = (GameObject)Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(effectInst, 1f);

        Vector3 dropPositionOffset = new Vector3(transform.position.x, transform.position.y + yOffset, transform.position.z);

        for (int i = 0; i < drops.Length; i++)
        {
            GameObject currentDrop = (GameObject)Instantiate(drops[i], dropPositionOffset, this.transform.rotation);
            Rigidbody rb = currentDrop.GetComponent<Rigidbody>();
            rb.AddForce(new Vector3(Random.Range(-force, force), force, Random.Range(-force, force)));
        }        
    }
	
}
