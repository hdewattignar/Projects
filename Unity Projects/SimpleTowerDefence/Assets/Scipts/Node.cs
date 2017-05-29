using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColour;
    public Color notEnoughMoneyColour;
    public Vector3 posOffset;

    [HideInInspector]
    public GameObject turret;
    [HideInInspector]
    public TurretBluePrint turretBluePrint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColour;

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        buildManager = BuildManager.instance;
    }

    void OnMouseDown()
    {
        //dont do anything if the cursor is over a UI button
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }        

        //if players selects a node that has a turret on it, bring up the UI. 
        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        //if player has no money
        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());

       
    }

    void BuildTurret(TurretBluePrint bluePrint)
    {
        if (PlayerStats.Money < bluePrint.cost)
        {
            Debug.Log("No Money Bro");
            return;
        }        

        PlayerStats.Money -= bluePrint.cost;

        GameObject _turret = (GameObject)Instantiate(bluePrint.preFab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;

        turretBluePrint = bluePrint;

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);
    }

    public void UpgradeTurret()
    {
        //make sure player has enough money
        if (PlayerStats.Money < turretBluePrint.upgradeCost)
        {
            Debug.Log("No Enough Money to Upgrade");
            return;
        }

        //subtract money from player
        PlayerStats.Money -= turretBluePrint.upgradeCost;

        //destroy current turret
        Destroy(turret);

        //build the new turret and set isUpgraded to true
        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);        
        isUpgraded = true;
        GameObject _turret = (GameObject)Instantiate(turretBluePrint.upgradedPreFab, GetBuildPosition(), Quaternion.identity);
        turret = _turret;   
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (!buildManager.CanBuild)
            return;

        if (buildManager.HasMoney)
        {
            rend.material.color = hoverColour;
        }
        else
        {
            rend.material.color = notEnoughMoneyColour;
        }
        
    }

    void OnMouseExit()
    {
        rend.material.color = startColour;
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + posOffset;
    }
}
