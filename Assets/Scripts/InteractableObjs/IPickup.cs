using System.Collections.Generic;
using UnityEngine;


public interface IPickup
{
  public string ItemName { get;  }
  public int ItemID { get;  }
  public int Amount { get;  }
  public Sprite ItemSprite { get; }

  public static List<IPickup> GetAllPickupIems()
  {
    List<IPickup> pickups = new List<IPickup>();
    MonoBehaviour[] allMonoBehaviours = GameObject.FindObjectsOfType<MonoBehaviour>();
    foreach (MonoBehaviour monoBehaviour in allMonoBehaviours)
    {
      if (monoBehaviour is IPickup pickup)
      {
        pickups.Add(pickup);
      }
    }
    if (pickups.Count == 0)
    {
      Debug.LogWarning("No IPickup items found in scene");
    }
    return pickups;
  }
}
