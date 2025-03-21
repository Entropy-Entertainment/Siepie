using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item 
{
    public string itemName;
    public int itemID;
    public Sprite icon;
    public int amount;

    public Item(string name, int id, int amount, Sprite icon = null)
    {
        this.itemName = name;
        this.itemID = id;
        this.amount = amount;
        this.icon = icon;
        
        
    }
}
