using UnityEngine;
using System.Collections;

public class ObstacleMovement : MonoBehaviour {

    public Rigidbody rb;
    public float sidewaysForce;
   

	// Update is called once per frame
	void FixedUpdate() {

        //rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

        //if (rb.position.x > 4 && sidewaysForce > 0)
        //{
        //    //rb.AddForce(0, 0, 0, ForceMode.VelocityChange);
            
        //    sidewaysForce = -sidewaysForce;
            
        //}
        //else if (rb.position.x < -4 && sidewaysForce < 0)
        //{
        //    //rb.AddForce(0, 0, 0, ForceMode.VelocityChange);
        //    sidewaysForce = -sidewaysForce;
        //}        
	}
}
