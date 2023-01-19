using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class SelectManager : MonoBehaviour
{
    private GameObject selectedObject;
    private BuildingManager buildingManager;
    private MeshRenderer objectMeshRenderer;

    [Header("UI")]
    public GameObject objectUI;
    public Image colorPreview;
    public TextMeshProUGUI objNameText;

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

                if (hit.transform.tag == "Object")
                {
                    Select(hit.transform.gameObject);
                }
                else if (selectedObject) Deselect();
            }
        }

        if (selectedObject && buildingManager.isBuilding) Deselect();

        // Deselect object by pressing right mouse button
        if (Input.GetMouseButtonDown(1) && selectedObject) Deselect();
    }

    private void Select(GameObject gameObject)
    {
        if (gameObject.GetComponent<CheckPlacement>().isPlaced && !buildingManager.isBuilding)
        {
            //set the object MeshRenderer variable
            objectMeshRenderer = gameObject.GetComponent<MeshRenderer>();

            // if object is already selected return
            if (selectedObject == gameObject) return;

            // if different object is already selected deselect it
            if (selectedObject != null) Deselect();

            //Get outline
            Outline outline = gameObject.GetComponent<Outline>();

            //If object doesn't have an aoutline add one and set active
            if (outline == null) gameObject.AddComponent<Outline>();
            else outline.enabled = true;

            // set UI to active, set display text and set the selected object
            objNameText.text = "Selected: " + Regex.Split(gameObject.name, "\\d")[0];
            objectUI.SetActive(true);
            selectedObject = gameObject;
            colorPreview.color = objectMeshRenderer.material.color;
        }
    }

    private void Deselect()
    {
        objectUI.SetActive(false);
        selectedObject.GetComponent<Outline>().enabled = false;
        selectedObject = null;
    }

    public void Move()
    {
        selectedObject.GetComponent<CheckPlacement>().isPlaced = false;
        buildingManager.SetSelectedObject(selectedObject);
    }

    public void Delete()
    {
        GameObject objToDestroy = selectedObject;
        DataManager.Instance.RemoveItem(selectedObject.name);
        Deselect();
        Destroy(objToDestroy);
    }

    public void ChangeColor(Color color)
    {
        objectMeshRenderer.material.color = color;
        DataManager.Instance.UpdateColor(selectedObject.name, color.ToString());
    }
}
