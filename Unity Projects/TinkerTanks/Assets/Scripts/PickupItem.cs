using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour {

    public float healthRegen = 10;

	void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<PlayerManager>().Heal(healthRegen);
            Destroy(this.gameObject);
        }        
    }
}
