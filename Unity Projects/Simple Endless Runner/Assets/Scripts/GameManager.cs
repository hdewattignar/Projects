using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public bool gameRunning = true;
    public float restartDelay = 1f;
    public GameObject completeLevelUI;    
    
    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
        FindObjectOfType<FollowPlayer>().StopFollow();
        FindObjectOfType<Score>().StopScore();
    }

    public void GameOver()
    {
        if(gameRunning == true)
        {
            gameRunning = false;
            Debug.Log("gameover");
            Restart();
            Invoke("Restart", restartDelay);
        }        
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
