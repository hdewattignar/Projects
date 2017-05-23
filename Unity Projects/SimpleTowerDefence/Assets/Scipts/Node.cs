using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour {

    public Color hoverColour;
    public Color notEnoughMoneyColour;
    public GameObject turret;
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

        if (!buildManager.CanBuild)
            return;

        if(turret != null)
        {
            Debug.Log("Cant do dat");
            return;
        }

        //build a turret
        buildManager.BuildTurretOn(this);
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
