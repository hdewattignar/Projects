using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    private TurretBluePrint turretToBuild;    
    public GameObject buildEffect;
    private Node selectedNode;

    public NodeUI nodeUI;

    void Awake()
    {
        if(instance != null)
        {
            return;
        }
            
        instance = this;
    }

    public bool CanBuild {  get {return turretToBuild != null; } }

    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } }
    
    public void SelectTurretToBuiild(TurretBluePrint turret)
    {
        turretToBuild = turret;
        DeselectNode();
    }

    public void SelectNode(Node node)
    {
        if (selectedNode == node)
        {
            DeselectNode();
            return;
        }

        selectedNode = node;
        turretToBuild = null;

        nodeUI.setTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public void BuildTurretOn(Node node)
    {
        if (PlayerStats.Money < turretToBuild.cost)
        {
            Debug.Log("No Money Bro");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;

        GameObject effect = (GameObject)Instantiate(buildEffect, node.GetBuildPosition(), Quaternion.identity);
        Destroy(effect, 5f);

        GameObject turret = (GameObject)Instantiate(turretToBuild.preFab, node.GetBuildPosition(), Quaternion.identity);
        node.turret = turret;

        Debug.Log("Money left = " + PlayerStats.Money);
    }
        
}
