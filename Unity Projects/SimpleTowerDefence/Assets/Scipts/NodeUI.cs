using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeUI : MonoBehaviour {

    private Node target;
    public GameObject ui;
    public Text upgradeCost;
    public Button upgradeButton;

    public void SetTarget(Node _target)
    {
        target = _target;

        transform.position = target.GetBuildPosition();        

        if(!target.isUpgraded)
        {
            upgradeCost.text = "$" + target.turretBluePrint.upgradeCost;
            upgradeButton.interactable = true;
        }
        else
        {
            upgradeCost.text = "DONE";
            upgradeButton.interactable = false;
        }

        ui.SetActive(true);
    }

    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        Debug.Log("Upgrade called");
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }
}
