using UnityEngine;

public class ObjectCollider : MonoBehaviour
{
    CheckPlacement placementScript;
    Vector3 sizeOfObject;
    BuildingManager buildingManager;

    private void Start()
    {
        placementScript = transform.parent.parent.GetComponent<CheckPlacement>();
        sizeOfObject = transform.parent.parent.GetComponent<Collider>().bounds.size;
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (buildingManager.isObjectSnapOn)
        {
            if (other.tag == "Object" && placementScript.isPlaced && !other.GetComponent<CheckPlacement>().isSnapped && !other.GetComponent<CheckPlacement>().isPlaced)
            {
                CheckPlacement otherObject = other.GetComponent<CheckPlacement>();

                otherObject.isSnapped = true;
                otherObject.mousePosX = Input.GetAxis("Mouse X");
                otherObject.mousePosY = Input.GetAxis("Mouse Y");

                float sizeX = sizeOfObject.x;
                float sizeZ = sizeOfObject.z;

                switch (this.transform.tag)
                {
                    case "EastCollider":
                        other.transform.position = new Vector3(transform.parent.parent.position.x + sizeX, other.transform.position.y, transform.parent.position.z);
                        break;
                    case "WestCollider":
                        other.transform.position = new Vector3(transform.parent.parent.position.x - sizeX, other.transform.position.y, transform.parent.position.z);
                        break;
                    case "NorthCollider":
                        other.transform.position = new Vector3(transform.parent.parent.position.x, other.transform.position.y, transform.parent.position.z + sizeZ);
                        break;
                    case "SouthCollider":
                        other.transform.position = new Vector3(transform.parent.parent.position.x, other.transform.position.y, transform.parent.position.z - sizeZ);
                        break;
                }
            }
        }
    }
}
