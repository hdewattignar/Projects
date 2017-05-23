using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MoneyUI : MonoBehaviour {

    public Text moneyText;
	
	// Update is called once per frame
	void Update () {

        moneyText.text = "$" + PlayerStats.Money.ToString();

	}
}
