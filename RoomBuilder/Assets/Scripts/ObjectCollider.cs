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
                        if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 90, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x, other.transform.position.y, transform.parent.position.z - sizeZ);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 180, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x - sizeX, other.transform.position.y, transform.parent.position.z);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 270, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x, other.transform.position.y, transform.parent.position.z + sizeZ);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 45, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x + Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z - Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 135, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x - Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z - Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 225, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x - Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z + Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 315, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x + Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z + Mathf.Sqrt(2) / 2);
                        }
                        else
                            other.transform.position = new Vector3(transform.parent.parent.position.x + sizeX, other.transform.position.y, transform.parent.position.z);
                        break;
                    case "WestCollider":
                        if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 90, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x, other.transform.position.y, transform.parent.position.z + sizeZ);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 180, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x + sizeX, other.transform.position.y, transform.parent.position.z);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 270, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x, other.transform.position.y, transform.parent.position.z - sizeZ);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 45, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x - Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z + Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 135, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x + Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z + Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 225, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x + Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z - Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 315, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x - Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z - Mathf.Sqrt(2) / 2);
                        }
                        else
                            other.transform.position = new Vector3(transform.parent.parent.position.x - sizeX, other.transform.position.y, transform.parent.position.z);
                        break;
                    case "NorthCollider":
                        if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 90, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x + sizeX, other.transform.position.y, transform.parent.position.z);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 180, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x, other.transform.position.y, transform.parent.position.z - sizeZ);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 270, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x - sizeX, other.transform.position.y, transform.parent.position.z);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 45, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x + Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z + Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 135, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x + Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z - Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 225, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x - Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z - Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 315, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x - Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z + Mathf.Sqrt(2) / 2);
                        }
                        else
                            other.transform.position = new Vector3(transform.parent.parent.position.x, other.transform.position.y, transform.parent.position.z + sizeZ);
                        break;
                    case "SouthCollider":
                        if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 90, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x - sizeX, other.transform.position.y, transform.parent.position.z);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 180, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x, other.transform.position.y, transform.parent.position.z + sizeZ);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 270, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x + sizeX, other.transform.position.y, transform.parent.position.z);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 45, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x - Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z - Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 135, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x - Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z + Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 225, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x + Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z + Mathf.Sqrt(2) / 2);
                        }
                        else if (Mathf.Abs(Quaternion.Dot(transform.parent.parent.transform.rotation, Quaternion.Euler(0, 315, 0))) > 0.9999f)
                        {
                            other.transform.position = new Vector3(transform.parent.parent.position.x + Mathf.Sqrt(2) / 2, other.transform.position.y, transform.parent.position.z - Mathf.Sqrt(2) / 2);
                        }
                        else
                            other.transform.position = new Vector3(transform.parent.parent.position.x, other.transform.position.y, transform.parent.position.z - sizeZ);

                        break;
                }

                other.transform.rotation = transform.parent.parent.transform.rotation;
            }
        }
    }
}
