using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<Item> itemList = new List<Item>();
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
    public void AddItem(Item item)
    {
        itemList.Add(item);
        Debug.Log(item.ItemName + " Added to the inventiry");
    }
    public void RemoveItame(Item item)
    {
        itemList.Remove(item);
    }
}
