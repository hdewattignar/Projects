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

	// Use this for initialization
	void Start () {

        gameRunning = true;
        pauseMenu.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
        healthBar.fillAmount = player.GetComponent<PlayerManager>().GetHealthPertentage();
        coolDown.fillAmount = player.GetComponent<PlayerManager>().GetCoolDownPercentage();        
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
        gameRunning = false;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;        
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

}
