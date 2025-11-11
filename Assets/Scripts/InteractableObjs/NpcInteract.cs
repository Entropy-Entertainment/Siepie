using System;
using UnityEngine;

namespace Player.Interaction
{
  public class NpcInteract : MonoBehaviour, IInteractable
  {
    public Action<GameObject, GameObject> WasInteractedWith;
    bool firstTime = true;

    void Start()
    {
      SubscribeToInteractEvent();
    }

    public void PlayerInteracted(GameObject player, GameObject interactedObject)
    {
      if (interactedObject == this.gameObject)
      { 
        WasInteractedWith.Invoke(player, this.gameObject);
      }
    }
    public void SubscribeToInteractEvent()
    {
      if (firstTime)
      {
        PlayerInteractor.PlayerInteract += PlayerInteracted;
        firstTime = false;
      }
    }

    void OnDestroy()
    {
      PlayerInteractor.PlayerInteract -= PlayerInteracted;
    }
  }
}