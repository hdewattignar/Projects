using System.Collections;
using UnityEngine;

[System.Serializable]
public class TurretBluePrint{

    public GameObject preFab;
    public int cost;
    public GameObject upgradedPreFab;
    public int upgradeCost;

    public int GetSellAmount()
    {
        return cost / 2;
    }
}


