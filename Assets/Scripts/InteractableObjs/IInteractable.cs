using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player.Interaction
{
  public interface IInteractable //All interactables must inherit from monobehaviour and implement this interface -Nora
  {
    public void SubscribeToInteractEvent();
    public void PlayerInteracted(GameObject player, GameObject interactedObject);
    public static List<IInteractable> GetAllInteractableItems()
    {
      List<IInteractable> interactable = new();
      MonoBehaviour[] allMonoBehaviours = GameObject.FindObjectsOfType<MonoBehaviour>();
      foreach (MonoBehaviour monoBehaviour in allMonoBehaviours)
      {
        if (monoBehaviour is IInteractable pickupThingy)
        {
          interactable.Add(pickupThingy);
        }
      }
      if (interactable.Count == 0)
      {
        Debug.LogWarning("No IInteractable items found in scene");
      }
      return interactable;
    }
  }
}