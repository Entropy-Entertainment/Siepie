using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Player.Interaction
{
  public class Inventory : MonoBehaviour
  {
    [SerializeField] public List<IPickup> itemList = new();
    public event Action<IPickup> OnItemAdded;
    public void AddItem(IPickup item) // Use this if you want to add an item - David
    {
      itemList.Add(item);
      OnItemAdded?.Invoke(item);
      Debug.Log($"Added {item.Amount} {item.ItemName}");
    }

    public bool RemoveItemByID(int itemID) // Use this to remove an item
    {
      IPickup itemtoremove = itemList.FirstOrDefault(item => item.ItemID == itemID);
      if (itemtoremove != null)
      {
        itemList.Remove(itemtoremove);
        Debug.Log($"Removed {itemtoremove.ItemName}");
        return true;
      }
      else
      {
        return false;
      }

    }

    public bool HasItemID(int ItemID) // Use this if you want to check for specific itemID 
    {
      return itemList.Any(Item => Item.ItemID == ItemID);
    }
  }
}
