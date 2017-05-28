using UnityEngine;


public class Shop : MonoBehaviour {

    public TurretBluePrint standardTurret;
    public TurretBluePrint missileTurret;
    public TurretBluePrint laserTurret;
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
        buildManager.SelectTurretToBuiild(laserTurret);
    }

   
}
