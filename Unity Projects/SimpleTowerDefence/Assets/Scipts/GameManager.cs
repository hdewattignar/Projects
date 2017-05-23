using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


    private bool running = true;

	// Update is called once per frame
	void Update () {

        if (!running)
        {
            return;
        }

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
		
	}

    void EndGame()
    {
        running = false;
        Debug.Log("Game Over");
    }
}
