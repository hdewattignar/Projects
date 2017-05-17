using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    private GameObject turretToBuild;
    public GameObject standardTurretPreFab;
    public GameObject missileTurretPreFab;
    void Awake()
    {
        if(instance != null)
        {
            return;
        }
            
        instance = this;
    }

    

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }

    public void SetTurretToBuiild(GameObject turret)
    {
        turretToBuild = turret;
    }
}
