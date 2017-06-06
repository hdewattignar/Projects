using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static bool GameRunning;
    public GameObject gameOverUI;
    public GameObject completeLevelUI;  
    

    void Start()
    {
        GameRunning = true;
    }

	// Update is called once per frame
	void Update () {

        if (!GameRunning)
        {
            return;
        }

        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
		
	}

    void EndGame()
    {
        GameRunning = false;

        gameOverUI.SetActive(true);
    }

    public void WinLevel()
    {
        Debug.Log("Level Won");
        GameRunning = false;
        completeLevelUI.SetActive(true);
        
    }
}
