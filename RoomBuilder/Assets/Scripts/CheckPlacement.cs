using UnityEngine;

public class CheckPlacement : MonoBehaviour
{
    private BuildingManager buildingManager;
    private GameObject colliders;
    public bool isPlaced = false;
    public bool isSnapped = false;
    public float mousePosX;
    public float mousePosY;

    void Start()
    {
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
        colliders = this.transform.GetChild(0).gameObject;
        colliders.SetActive(false);
    }

    private void Update()
    {
        if (isSnapped && !isPlaced && Mathf.Abs(mousePosX - Input.GetAxis("Mouse X")) > 0.05f || Mathf.Abs(mousePosY - Input.GetAxis("Mouse Y")) > 0.05f)
        {
            isSnapped = false;
        }

        if (isPlaced)
        {
            colliders.SetActive(true);
        }
        else { colliders.SetActive(false); }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPlaced)
        {
            if (buildingManager.isObjectSnapOn)
            {
                if (other.tag == "Wall")
                {
                    buildingManager.canPlace = false;
                }
            }
            else if (other.tag == "Object" || other.tag == "Wall")
            {
                buildingManager.canPlace = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isPlaced && buildingManager.isObjectSnapOn && other.tag == "Wall")
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
