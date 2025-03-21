using Unity.VisualScripting;
using UnityEngine;


namespace Player.Interaction
{
  public class GeneralPickup : MonoBehaviour, IPickup, IInteractable
  {
    public string ItemName { get => gameObject.name; }
    public int ItemID { get => itemID; }
    public int Amount { get => amount; }
    public Sprite ItemSprite { get => itemSprite; }

    [SerializeField] int itemID;
    [SerializeField] int amount;
    [SerializeField] Sprite itemSprite;

    void Start()
    {
      SubscribeToInteractEvent();
    }
    public void SubscribeToInteractEvent()
    {
      PlayerInteractor.PlayerInteract += PlayerInteracted;
    }

    public void PlayerInteracted(GameObject player, GameObject interactedObject)
    {
      if (interactedObject == this.gameObject)
      {
        player.GetComponent<Inventory>().AddItem(this);
        gameObject.SetActive(false);
        PlayerInteractor.PlayerInteract -= PlayerInteracted;
      }   
    }
  }
}