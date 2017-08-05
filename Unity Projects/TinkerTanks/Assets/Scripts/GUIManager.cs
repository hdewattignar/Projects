using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIManager : MonoBehaviour {

    GameObject manager;

    void Start()
    {
        manager = FindObjectOfType<Manager>().gameObject;
    }

    void Update()
    {
        if (Input.GetButton("StartButton"))
        {
            manager.GetComponent<Manager>().Pause();
            Debug.Log("pause");
        }

        if (Input.GetButton("BackButton"))
        {

        }
    }
}
