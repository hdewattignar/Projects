using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

    public enum InputTypes {Button, LeftStick, RightStick};

    public Material inActiveMat;
    public Material activeMat;

    public InputTypes input;
    public string name;

    Vector3 startPos;
    	
    void Start()
    {
        startPos = this.transform.position;
    }

	// Update is called once per frame
	void Update () {


        if (input == InputTypes.Button)
        {
            if (Input.GetButton(name))
            {
                this.transform.GetComponent<Renderer>().material = activeMat;
            }
            else
            {
                this.transform.GetComponent<Renderer>().material = inActiveMat;
            }
        }
        else
        {
            if (input == InputTypes.LeftStick)
            {
                Vector3 leftStickMovement = Vector3.zero;
                leftStickMovement.x = Input.GetAxis("LeftStickHorizontal");
                leftStickMovement.z = Input.GetAxis("LeftStickVertical");
                this.transform.position = startPos + leftStickMovement;
            }
            else
            {
                Vector3 rightStickMovement = Vector3.zero;
                rightStickMovement.x = Input.GetAxis("RightStickHorizontal");
                rightStickMovement.z = Input.GetAxis("RightStickVertical");
                this.transform.position = startPos + rightStickMovement;
            }
        }

        
	}
}
