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

        nodeUI.SetTarget(node);
    }

    public void DeselectNode()
    {
        selectedNode = null;
        nodeUI.Hide();
    }

    public TurretBluePrint GetTurretToBuild()
    {
        return turretToBuild;
    }
}
