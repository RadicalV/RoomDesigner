using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    private BuildingManager buildingManager;
    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Object" || other.tag == "Wall")
        {
            buildingManager.canPlace = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Object" || other.tag == "Wall")
        {
            buildingManager.canPlace = true;
        }
    }
}
