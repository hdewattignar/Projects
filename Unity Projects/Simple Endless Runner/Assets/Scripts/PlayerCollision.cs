using UnityEngine;
using System.Collections;

public class PlayerCollision : MonoBehaviour {

    public PLayerMovement movement;

	void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "Obstacle")
        {
            movement.enabled = false;
            FindObjectOfType<GameManager>().GameOver();
            
        }
    }
}
