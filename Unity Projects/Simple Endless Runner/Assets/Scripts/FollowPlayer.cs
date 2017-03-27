using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public Transform playerPos;
    public bool followPlayer = true; 
    public Vector3 offset;
	
	// Update is called once per frame
	void Update () {        

        if(followPlayer)
        {
            transform.position = playerPos.position + offset;
        }
        
	}

    public void StopFollow()
    {
        followPlayer = false;
    }

    public void StartFollow()
    {
        followPlayer = true;
    }
}
