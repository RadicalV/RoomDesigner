using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class SelectManager : MonoBehaviour
{

    public GameObject selectedObject;
    public TextMeshProUGUI objNameText;
    private BuildingManager buildingManager;
    public GameObject objectUI;

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    void Update()
    {
        // select an object by press left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                if (hit.collider.tag == "Object")
                {
                    Select(hit.collider.gameObject);
                }
                else if (selectedObject) Deselect();
            }
        }

        // Deselect object by pressing right mouse button
        if (Input.GetMouseButtonDown(1) && selectedObject) Deselect();
    }

    private void Select(GameObject gameObject)
    {
        // if object is already selected return
        if (selectedObject == gameObject) return;

        // if different object is already selected deselect it
        if (selectedObject != null) Deselect();

        //Get outline
        Outline outline = gameObject.GetComponent<Outline>();

        //If object doesn't have an aoutline add one
        if (outline == null) gameObject.AddComponent<Outline>();
        else outline.enabled = true;

        objNameText.text = gameObject.name;
        objectUI.SetActive(true);
        selectedObject = gameObject;
    }

    private void Deselect()
    {
        objectUI.SetActive(false);
        selectedObject.GetComponent<Outline>().enabled = false;
        selectedObject = null;
    }

    public void Move()
    {
        buildingManager.SetSelectedObject(selectedObject);
    }

    public void Delete()
    {
        GameObject objToDestroy = selectedObject;
        Deselect();
        Destroy(objToDestroy);
    }
}
