using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColour;

    private GameObject turret;
    public Vector3 posOffset;
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (buildManager.GetTurretToBuild() == null)
            return;

        if(turret != null)
        {
            Debug.Log("Cant do dat");
            return;
        }

        //build a turret
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject) Instantiate(turretToBuild, transform.position + posOffset, transform.rotation);
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (buildManager.GetTurretToBuild() == null)
            return;

        rend.material.color = hoverColour;
    }

    void OnMouseExit()
    {
        rend.material.color = startColour;
    }
}
