using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

    public Color hoverColour;

    private GameObject turret;
    public Vector3 posOffset;
    private Renderer rend;
    private Color startColour;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
    }

    void OnMouseDown()
    {
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
        rend.material.color = hoverColour;
    }

    void OnMouseExit()
    {
        rend.material.color = startColour;
    }
}
