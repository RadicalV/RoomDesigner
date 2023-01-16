using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    public GameObject[] objects;
    private GameObject selectedObject;
    private Vector3 placementPos;
    private RaycastHit hit;

    [SerializeField]
    private LayerMask layer;

    public float gridSize;
    bool gridOn = false;

    [SerializeField]
    private Toggle gridToggle;


    // Update is called once per frame
    void Update()
    {
        if (selectedObject)
        {
            if (gridOn)
            {
                selectedObject.transform.position = new Vector3(RoundToNearestGrid(placementPos.x), RoundToNearestGrid(placementPos.y), RoundToNearestGrid(placementPos.z));
            }
            else
            {
                selectedObject.transform.position = placementPos;
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlaceObject();
            }
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layer))
        {
            placementPos = hit.point;
        }
    }

    public void SelectObject(int index)
    {
        selectedObject = Instantiate(objects[index], placementPos, transform.rotation);
    }

    public void PlaceObject()
    {
        selectedObject = null;
    }

    public void ToggleGrid()
    {
        if (gridToggle.isOn)
        {
            gridOn = true;
        }
        else { gridOn = false; }
    }

    float RoundToNearestGrid(float position)
    {
        float xDiff = position % gridSize;
        position -= xDiff;

        if (xDiff > (gridSize / 2))
        {
            position += gridSize;
        }

        return position;
    }
}
