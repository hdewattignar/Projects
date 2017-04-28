using UnityEngine;
using System.Collections;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;
    private GameObject turretToBuild;
    public GameObject standardTurretPreFab;

    void Awake()
    {
        if(instance != null)
        {
            return;
        }
            
        instance = this;
    }

    void Start()
    {
        turretToBuild = standardTurretPreFab;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
