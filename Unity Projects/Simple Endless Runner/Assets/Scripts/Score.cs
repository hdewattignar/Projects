using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    public Transform player;
    public Text score;

    bool keepScore = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if(keepScore)
            score.text = player.position.z.ToString("0");

	}

    public void StopScore()
    {
        keepScore = false;
    }

    public void StartScore()
    {
        keepScore = true;
    }
}
