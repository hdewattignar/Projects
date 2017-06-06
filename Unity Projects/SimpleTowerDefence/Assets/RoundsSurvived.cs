using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundsSurvived : MonoBehaviour {
    
    public Text roundstext;
    void OnEnable()
    {
        roundstext.text = PlayerStats.Waves.ToString();
    }    
}
