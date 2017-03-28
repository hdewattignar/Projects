using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class LevelComplete : MonoBehaviour {

	public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
