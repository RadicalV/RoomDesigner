using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    [Header("Placement options")]
    public GameObject[] objects;
    private GameObject selectedObject;
    private float selectedObjectHeight;
    private Vector3 placementPos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layer;
    public float rotateAmount;
    public bool canPlace = true;
    [Header("Visualization")]
    [SerializeField] private Material[] materials;

    [Header("Snap settings")]
    public float gridSize;
    bool isSnapOn = false;
    [SerializeField] private Toggle snapToggle;


    // Update is called once per frame
    void Update()
    {
        if (selectedObject)
        {
            // Moving the object
            if (isSnapOn)
            {
                selectedObject.transform.position = new Vector3(RoundToNearestGrid(placementPos.x), RoundToNearestGrid(placementPos.y), RoundToNearestGrid(placementPos.z));
            }
            else
            {
                selectedObject.transform.position = placementPos;
            }

            UpdateMaterials();

            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }

            if (Input.GetKeyDown(KeyCode.R)) { RotateObject(); }
        }
    }

    private void FixedUpdate()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000, layer))
        {
            if (selectedObject)
            {
                placementPos = new Vector3(hit.point.x, hit.point.y + selectedObjectHeight / 2, hit.point.z);
            }
        }
    }

    public void SelectObject(int index)
    {
        if (selectedObject) DeselectObject();

        selectedObject = Instantiate(objects[index], placementPos, transform.rotation);
        selectedObjectHeight = selectedObject.GetComponent<MeshRenderer>().bounds.size.y;
    }

    public void DeselectObject()
    {
        GameObject objToDestroy = selectedObject;
        Destroy(objToDestroy);
        selectedObject = null;
    }

    public void SetSelectedObject(GameObject obj)
    {
        selectedObject = obj;
        selectedObjectHeight = selectedObject.GetComponent<MeshRenderer>().bounds.size.y;
        placementPos = obj.transform.position;
    }

    public void PlaceObject()
    {
        selectedObject.GetComponent<MeshRenderer>().material = materials[2];
        selectedObject = null;
    }

    public void RotateObject()
    {
        selectedObject.transform.Rotate(Vector3.up, rotateAmount);
    }

    public void ToggleGrid()
    {
        if (snapToggle.isOn)
        {
            isSnapOn = true;
        }
        else { isSnapOn = false; }
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

    void UpdateMaterials()
    {
        if (canPlace)
        {
            selectedObject.GetComponent<MeshRenderer>().material = materials[0];
        }
        else
        {
            selectedObject.GetComponent<MeshRenderer>().material = materials[1];
        }
    }
}
