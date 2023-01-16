using UnityEngine;
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

            if (Input.GetMouseButtonDown(0))
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
        selectedObject = Instantiate(objects[index], placementPos, transform.rotation);
        selectedObjectHeight = selectedObject.GetComponent<MeshRenderer>().bounds.size.y;
    }

    public void PlaceObject()
    {
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
}
