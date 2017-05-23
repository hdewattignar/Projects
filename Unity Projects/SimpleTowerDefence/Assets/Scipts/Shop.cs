using UnityEngine;


public class Shop : MonoBehaviour {

    public TurretBluePrint standardTurret;
    public TurretBluePrint missileTurret;
    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SelectStandardTurret()
    {
        buildManager.SelectTurretToBuiild(standardTurret);
    }

    public void SelectMissileTurret()
    {
        buildManager.SelectTurretToBuiild(missileTurret);
    }

    public void SelectLaserTurret()
    {
        Debug.Log("laser Turret");
    }
}
