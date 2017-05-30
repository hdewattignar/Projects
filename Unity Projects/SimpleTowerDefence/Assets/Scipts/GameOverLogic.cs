using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverLogic : MonoBehaviour {

	public Text roundstext;
    public SceneFader sceneFader;
    public string menuSceneName = "MainMenu";

    void OnEnable()
    {
        roundstext.text = PlayerStats.Waves.ToString();
    }

    public void Retry()
    {
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}
