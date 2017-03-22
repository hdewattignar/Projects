using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    bool gameRunning = true;

    public float restartDelay = 1f;

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
