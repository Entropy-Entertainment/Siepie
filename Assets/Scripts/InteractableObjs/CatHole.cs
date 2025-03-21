using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Player.Interaction
{
  public class CatHole : MonoBehaviour, IInteractable
  {
    public Transform Destination;
    private void Start()
    {
      SubscribeToInteractEvent();
    }
    public void SubscribeToInteractEvent()
    {
      PlayerInteractor.PlayerInteract += PlayerInteracted;
    }
    public void PlayerInteracted(GameObject player, GameObject interactedObject)
    {
      if (interactedObject == this.gameObject && player.CompareTag("Cat"))
      {
        //Positions Siepie to the specified transform.position. -Henry
        player.transform.position = Destination.transform.position;
        Debug.Log(this.gameObject.name + " Teleporting");
      }
    }
  }
}
