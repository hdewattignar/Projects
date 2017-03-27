using UnityEngine;
using System.Collections;

public class EndTrigger : MonoBehaviour {

    public GameManager gm;

	void OnTriggerEnter()
    {
        gm.CompleteLevel();
    }
}
