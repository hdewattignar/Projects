using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockColourRandomizer : MonoBehaviour {

    public Material[] colours;

	// Use this for initialization
	void Start () {

        int rnd = Random.Range(0, colours.Length);

        GetComponent<Renderer>().material = colours[rnd];

	}

    void FixedUpdate()
    {
        if (transform.position.y < -100)
        {
            Destroy(gameObject);
        }
    }
}
