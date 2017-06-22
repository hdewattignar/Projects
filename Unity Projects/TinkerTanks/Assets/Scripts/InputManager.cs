using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    public GameObject tankInputDirection;
    public GameObject turretInputDirection;
    public Transform partToRotate;
    public Transform firePoint;
    public GameObject bulletPreFab;
    
    public float bulletCoolDown = 1;
    public float turnSpeed = 10f;
    public float moveSpeed = 10f;

    int stickOffset = 15; //adds more distance to the stick values for better
    Component playerManager;

    void Start()
    {
        playerManager = GetComponentInParent<PlayerManager>();
    }

	// Update is called once per frame
	void Update () {

        GetControlInputs();

        if (bulletCoolDown < 1)
        {
            bulletCoolDown += Time.deltaTime;
        }

	}

    void GetControlInputs()
    {
        //Tank rotation
        Vector3 rightStickRotation = Vector3.zero;
        rightStickRotation.x = Input.GetAxis("RightStickHorizontal");
        rightStickRotation.z = Input.GetAxis("RightStickVertical");
        tankInputDirection.transform.position = this.transform.position + (new Vector3(rightStickRotation.x * stickOffset, 0, rightStickRotation.z * stickOffset));

        //Turret rotation
        Vector3 leftStickRotation = Vector3.zero;
        leftStickRotation.x = Input.GetAxis("LeftStickHorizontal");
        leftStickRotation.z = Input.GetAxis("LeftStickVertical");
        turretInputDirection.transform.position = partToRotate.transform.position + (new Vector3(leftStickRotation.x * stickOffset, 0, leftStickRotation.z * stickOffset));

        CalculateRotation();

        //drive forward       
        this.transform.Translate(Vector3.forward * (moveSpeed * -Input.GetAxis("RightTrigger")), Space.Self);

        //drive back
        this.transform.Translate(Vector3.back * (moveSpeed * Input.GetAxis("LeftTrigger")), Space.Self);

        //fire
        if (Input.GetButton("LeftShoulderButton") && bulletCoolDown >= 1)
        {
            FireTurret();
        }
    }

    void CalculateRotation()
    {

        //check input direction is not zero
        if (tankInputDirection.transform.position != this.transform.position)
        {
            Vector3 dir = tankInputDirection.transform.position - this.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = (Quaternion.Lerp(this.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles);
            this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }

        if (turretInputDirection.transform.position != this.transform.position)
        {
            Vector3 dir = turretInputDirection.transform.position - this.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = (Quaternion.Lerp(partToRotate.transform.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles);
            partToRotate.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }        
    }

    void FireTurret()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPreFab, firePoint.position, firePoint.rotation);
        bulletCoolDown = 0;
    }
}
