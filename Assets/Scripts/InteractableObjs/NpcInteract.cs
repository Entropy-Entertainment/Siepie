using UnityEngine;

namespace Player.Interaction
{
  public class NpcInteract : MonoBehaviour, IInteractable
  {
    // Start is called before the first frame update
    void Start()
    {
      SubscribeToInteractEvent();
    }

    public void PlayerInteracted(GameObject player, GameObject interactedObject)
    {
      throw new System.NotImplementedException();
    }

    public void SubscribeToInteractEvent()
    {
      PlayerInteractor.PlayerInteract += PlayerInteracted;
    }
  }
}