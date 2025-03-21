using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    public List<Item> itemList;

    public void AddItem(Item item) // Use this if you want to add an item - David
    {
        itemList.Add(item);
        Debug.Log($"Added {item.amount} { item.itemName}");
    }

    public bool RemoveItemByID(int itemID) // Use this to remove an item
    {
        Item itemtoremove = itemList.FirstOrDefault(item => item.itemID == itemID);
        if (itemtoremove != null)
        {
            itemList.Remove(itemtoremove);
            Debug.Log($"Removed { itemtoremove.itemName}");
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public bool HasItemID(int ItemID) // Use this if you want to check for specific itemID 
    {
        return itemList.Any(Item => Item.itemID == ItemID);
    }


}
