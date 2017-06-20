using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonActivate : MonoBehaviour{

    public Material activeMaterial;
    public Material inActiveMaterial;

    public GameObject Abutton;
    public GameObject Bbutton;
    public GameObject Xbutton;
    public GameObject Ybutton;
    public GameObject Startbutton;
    public GameObject Backbutton;

    public GameObject RightStick;
    public GameObject LeftStick;	
	
	// Update is called once per frame
    //void Update () {

    //    //Buttons

    //    // A button
    //    if (Input.GetButton("A_button"))
    //    {
    //        Abutton.GetComponent<Renderer>().material = activeMaterial;
    //    }
    //    else
    //    {
    //        Abutton.GetComponent<Renderer>().material = inActiveMaterial;
    //    }

    //    // B Button
    //    if (Input.GetButton("B_button"))
    //    {
    //        Bbutton.GetComponent<Renderer>().material = activeMaterial;
    //    }
    //    else
    //    {
    //        Bbutton.GetComponent<Renderer>().material = inActiveMaterial;
    //    }

    //    // X button
    //    if (Input.GetButton("X_button"))
    //    {
    //        Xbutton.GetComponent<Renderer>().material = activeMaterial;
    //    }
    //    else
    //    {
    //        Xbutton.GetComponent<Renderer>().material = inActiveMaterial;
    //    }

    //    // Y button
    //    if (Input.GetButton("Y_button"))
    //    {
    //        Ybutton.GetComponent<Renderer>().material = activeMaterial;
    //    }
    //    else
    //    {
    //        Ybutton.GetComponent<Renderer>().material = inActiveMaterial;
    //    }

    //    // Start button
    //    if (Input.GetButton("Start_button"))
    //    {
    //        Startbutton.GetComponent<Renderer>().material = activeMaterial;
    //    }
    //    else
    //    {
    //        Startbutton.GetComponent<Renderer>().material = inActiveMaterial;
    //    }

    //    // Back button
    //    if (Input.GetButton("Back_button"))
    //    {
    //        Backbutton.GetComponent<Renderer>().material = activeMaterial;
    //    }
    //    else
    //    {
    //        Backbutton.GetComponent<Renderer>().material = inActiveMaterial;
    //    }

    //    //Sticks

    //    //Right
    //    Vector3 rightStickMovement = Vector3.zero;
    //    rightStickMovement.x = Input.GetAxis("RightStickHorizontal");
    //    rightStickMovement.z = Input.GetAxis("RightStickVertical");
    //    RightStick.transform.position += rightStickMovement;

    //    //Left
    //    Vector3 leftStickMovement = Vector3.zero;
    //    leftStickMovement.x = Input.GetAxis("LeftStickHorizontal");
    //    leftStickMovement.z = Input.GetAxis("LeftStickVertical");
    //    LeftStick.transform.position += leftStickMovement;

    //}
}
