using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

    public Image healthBar;
    public Image coolDown;
    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        healthBar.fillAmount = player.GetComponent<PlayerManager>().GetHealthPertentage();
        coolDown.fillAmount = player.GetComponent<PlayerManager>().GetCoolDownPercentage();
        
    }
}
