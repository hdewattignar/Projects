using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public Transform partToRotate;
    public float turnSpeed = 10f;
    public float moveSpeed = 10f;  

	// Update is called once per frame
	void Update () {

        GetControlInputs();
	}

    void GetControlInputs()
    {
        //Tank rotation
        Vector3 leftStickRotation = Vector3.zero;
        leftStickRotation.x = Input.GetAxis("LeftStickHorizontal");
        leftStickRotation.z = Input.GetAxis("LeftStickVertical");        
        this.transform.rotation *= Quaternion.Euler(0f, leftStickRotation.x * turnSpeed, 0f);

        //Turret rotation
        Vector3 rightStickRotation = Vector3.zero;
        rightStickRotation.x = Input.GetAxis("RightStickHorizontal");
        rightStickRotation.z = Input.GetAxis("RightStickVertical");
        partToRotate.transform.rotation *= Quaternion.Euler(0f, rightStickRotation.x * turnSpeed, 0f);   
     
        float rightTriggerInput = -Input.GetAxis("RightTrigger");
        float leftTriggerInput = Input.GetAxis("RightTrigger");
        if (rightTriggerInput > 0.8f)
        {
            rightTriggerInput = 1;
        }
        if (leftTriggerInput > 0.8f)
        {
            leftTriggerInput = 1;
        }


        //drive forward       
        this.transform.Translate(Vector3.forward * (moveSpeed * rightTriggerInput), Space.Self);

        //drive back
        this.transform.Translate(Vector3.back * (moveSpeed * leftTriggerInput), Space.Self);

        //fire
        if (Input.GetButton("LeftShoulderButton"))
        {
            GetComponentInParent<PlayerManager>().FireTurret();
        }
    }    

    
}
