using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Image healthBar;
    public Image coolDown;
    public GameObject player;
    static bool gameRunning = false;
    public SceneFader sf;
    public GameObject pauseMenu;    
    public Transform[] waypoints;
    public Button continueButton;

	// Use this for initialization
	void Start () {

        gameRunning = true;
        pauseMenu.SetActive(false);        
	}
	
	// Update is called once per frame
	void Update () {

        if (player != null)
        {
            healthBar.fillAmount = player.GetComponent<PlayerManager>().GetHealthPertentage();
            coolDown.fillAmount = player.GetComponent<PlayerManager>().GetCoolDownPercentage();
        }
        else
        {
            healthBar.fillAmount = 0;
            coolDown.fillAmount = 0;
            Debug.Log("no player");
        }
        

        if (player == null)
        {
            DisplayGameOverMenu();
            Debug.Log("no player");
        }
        
    }

    public void DisplayGameOverMenu()
    {
        continueButton.interactable = false;
        pauseMenu.SetActive(true);
    }

    public void DisplayPauseMenu()
    {
        gameRunning = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Retry()
    {
        Time.timeScale = 1;
        sf.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Time.timeScale = 1;
        sf.FadeTo("MainMenu");
    }

    public void Pause()
    {
        DisplayPauseMenu();
    }

    public void Continue()
    {
        pauseMenu.SetActive(false);
        gameRunning = true;
        Time.timeScale = 1;
    }

    public static bool IsGameRunning()
    {
        return gameRunning;
    }

    public Transform[] GetWayPoints()
    {
        return waypoints;
    }

    public void SetPlayer(GameObject newPlayer){

        player = newPlayer;        
    }

}
