using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildingManager : MonoBehaviour
{
    private MeshRenderer selectedObjectMeshRenderer;
    [Header("Placement options")]
    public GameObject[] objects;
    private GameObject selectedObject;
    private float selectedObjectHeight;
    private Vector3 placementPos;
    private RaycastHit hit;
    [SerializeField] private LayerMask layer;
    public float rotateAmount;
    public bool canPlace = true;
    public bool isBuilding;
    private bool isMoving;

    [Header("Visualization")]
    [SerializeField] private Material[] materials;
    public Image colorPreview;
    private Color selectedObjectColor;

    [Header("Snap settings")]
    public float gridSize;
    bool isGridSnapOn = false;
    [SerializeField] private Toggle gridSnapToggle;
    public bool isObjectSnapOn = false;
    [SerializeField] protected Toggle objectSnapToggle;

    // Update is called once per frame
    void Update()
    {
        if (selectedObject)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            // Moving the object
            if (isGridSnapOn)
            {
                selectedObject.transform.position = new Vector3(RoundToNearestGrid(placementPos.x), RoundToNearestGrid(placementPos.y), RoundToNearestGrid(placementPos.z));
            }
            else
            {
                if (!selectedObject.GetComponent<CheckPlacement>().isSnapped)
                    selectedObject.transform.position = placementPos;
            }

            UpdateMaterials();

            if (Input.GetMouseButtonDown(0) && canPlace)
            {
                PlaceObject();
            }

            if (Input.GetKeyDown(KeyCode.R)) { RotateObject(); }
        }
    }

    private void FixedUpdate()
    {
        //Gets mouse position and sets object placement position to it
        if (selectedObject)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000, layer))
            {
                placementPos = new Vector3(hit.point.x, hit.point.y + selectedObjectHeight / 2, hit.point.z);
            }
        }
    }

    // Sets selected object based on the UI button pressed
    public void SelectObject(int index)
    {
        if (isMoving) return;

        if (selectedObject) DeselectObject();

        selectedObject = Instantiate(objects[index], placementPos, transform.rotation);
        selectedObject.name = objects[index].name;

        selectedObjectMeshRenderer = selectedObject.GetComponent<MeshRenderer>();
        selectedObjectHeight = selectedObjectMeshRenderer.bounds.size.y;
        selectedObjectColor = materials[2].color;
        isBuilding = true;
    }

    // Destroys selected object if it's not placed
    public void DeselectObject()
    {
        if (!isMoving)
        {
            isBuilding = false;
            GameObject objToDestroy = selectedObject;
            Destroy(objToDestroy);
            selectedObject = null;
        }
    }

    public void SetSelectedObject(GameObject obj)
    {
        // set selected object and mesh renderer to newly selected object
        selectedObject = obj;
        selectedObjectMeshRenderer = obj.GetComponent<MeshRenderer>();

        selectedObjectHeight = selectedObjectMeshRenderer.bounds.size.y;
        selectedObjectColor = selectedObjectMeshRenderer.material.color;
        placementPos = obj.transform.position;
        isBuilding = true;
        isMoving = true;
    }

    //Places the selected object
    public void PlaceObject()
    {
        isBuilding = false;
        isMoving = false;

        selectedObjectMeshRenderer.material.color = selectedObjectColor;
        colorPreview.color = selectedObjectColor;

        selectedObject.GetComponent<CheckPlacement>().isPlaced = true;
        DataManager.Instance.AddItem(selectedObject, selectedObject.name);

        selectedObject = null;
    }

    public void RotateObject()
    {
        selectedObject.transform.Rotate(Vector3.up, rotateAmount);
    }

    public void ToggleGrid()
    {
        if (gridSnapToggle.isOn)
        {
            isGridSnapOn = true;
        }
        else { isGridSnapOn = false; }
    }

    public void ToggleObjectSnap()
    {
        if (objectSnapToggle.isOn)
        {
            isObjectSnapOn = true;
        }
        else { isObjectSnapOn = false; }
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
            selectedObjectMeshRenderer.material = materials[0];
        }
        else
        {
            selectedObjectMeshRenderer.material = materials[1];
        }
    }
}
