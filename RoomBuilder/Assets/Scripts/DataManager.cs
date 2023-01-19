using System.Collections.Generic;
using UnityEngine;

using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.SceneManagement;
using System.Linq;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance { get; set; }
    private BuildingManager buildingManager;
    public ItemDB itemDB;
    void Start()
    {
        Instance = this;
        buildingManager = GameObject.Find("BuildingManager").GetComponent<BuildingManager>(); ;
    }

    public void AddItem(GameObject obj, string itemId)
    {
        bool found = false;
        foreach (Item it in itemDB.items)
        {
            if (it.ItemID == itemId)
            {
                found = true;
                it.Position = obj.transform.position;
                it.Rotation = obj.transform.rotation;
            }
        }

        if (!found)
        {
            Item item = new Item();
            item.PrefabID = obj.name;
            item.ItemID = item.PrefabID + itemDB.items.Count + ToString();
            obj.name = item.ItemID;
            item.Position = obj.transform.position;
            item.Rotation = obj.transform.rotation;
            item.Color = obj.GetComponent<MeshRenderer>().material.color.ToString();
            itemDB.items.Add(item);
        }
    }

    public void RemoveItem(string itemId)
    {
        foreach (Item item in itemDB.items.ToList())
        {
            if (item.ItemID == itemId)
            {
                itemDB.items.Remove(item);
            }
        }
    }

    public void UpdateColor(string itemId, string color)
    {
        foreach (Item item in itemDB.items)
        {
            if (item.ItemID == itemId)
            {
                item.Color = color;
            }
        }
    }

    public void SaveData()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/Saves");
        }
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemDB));
        FileStream stream = new FileStream(Application.persistentDataPath + "/Saves/Game_Data.xml", FileMode.Create);
        xmlSerializer.Serialize(stream, itemDB);
        stream.Close();
    }

    public void LoadData()
    {
        if (itemDB.items.Count != 0)
        {
            foreach (Item item in itemDB.items)
            {
                GameObject toDelete = GameObject.Find(item.ItemID);

                Destroy(toDelete);
            }
        }

        if (!File.Exists(Application.persistentDataPath + "/Saves/Game_Data.xml")) return;

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(ItemDB));
        FileStream stream = new FileStream(Application.persistentDataPath + "/Saves/Game_Data.xml", FileMode.Open);
        itemDB = xmlSerializer.Deserialize(stream) as ItemDB;
        stream.Close();

        foreach (Item item in itemDB.items)
        {
            switch (item.PrefabID)
            {
                case "Cube":
                    GameObject go = Instantiate(buildingManager.objects[0], item.Position, item.Rotation);
                    string[] rgba = item.Color.Substring(5, item.Color.Length - 6).Split(", ");
                    go.GetComponent<MeshRenderer>().material.color = new Color(float.Parse(rgba[0]), float.Parse(rgba[1]), float.Parse(rgba[2]), float.Parse(rgba[3]));
                    go.GetComponent<CheckPlacement>().isPlaced = true;
                    go.name = item.ItemID;
                    break;
                case "Cylinder":
                    GameObject go1 = Instantiate(buildingManager.objects[1], item.Position, item.Rotation);
                    string[] rgba1 = item.Color.Substring(5, item.Color.Length - 6).Split(", ");
                    go1.GetComponent<MeshRenderer>().material.color = new Color(float.Parse(rgba1[0]), float.Parse(rgba1[1]), float.Parse(rgba1[2]), float.Parse(rgba1[3]));
                    go1.GetComponent<CheckPlacement>().isPlaced = true;
                    go1.name = item.ItemID;
                    break;
                case "Sphere":
                    GameObject go2 = Instantiate(buildingManager.objects[2], item.Position, item.Rotation);
                    string[] rgba2 = item.Color.Substring(5, item.Color.Length - 6).Split(", ");
                    go2.GetComponent<MeshRenderer>().material.color = new Color(float.Parse(rgba2[0]), float.Parse(rgba2[1]), float.Parse(rgba2[2]), float.Parse(rgba2[3]));
                    go2.GetComponent<CheckPlacement>().isPlaced = true;
                    go2.name = item.ItemID;
                    break;
            }


        }
    }

    public void NewScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

[System.Serializable]
public class ItemDB
{
    public List<Item> items = new List<Item>();
}

[System.Serializable]
public class Item
{
    public string PrefabID;
    public string ItemID;
    public Vector3 Position;
    public Quaternion Rotation;
    public string Color;
}
