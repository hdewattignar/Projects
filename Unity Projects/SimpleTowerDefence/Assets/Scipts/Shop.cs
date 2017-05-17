using UnityEngine;


public class Shop : MonoBehaviour {

    BuildManager buildManager;

    void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void PurchaseStandardTurret()
    {
        buildManager.SetTurretToBuiild(buildManager.standardTurretPreFab);
    }

    public void PurchaseMissileTurret()
    {
        buildManager.SetTurretToBuiild(buildManager.missileTurretPreFab);
    }

    public void PurchaseLaserTurret()
    {
        Debug.Log("laser Turret");
    }
}
