using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractablePickup : PlayerInteraction
{
    public GameObject takkie;
    public Animator animator;
    public SpriteRenderer thisObjSprite;

    public override void PlayerInteracted(GameObject player, GameObject interactedObject)
    {
        if(player.CompareTag("Human") && interactedObject == this.gameObject)
        {
            Debug.Log("Interacting");
            TakkiePickup.spriteToShow = thisObjSprite.sprite;
            animator.SetTrigger("Pickup");
            
            

            Item newItem = new Item("Potato",1,1,thisObjSprite.sprite);

            Inventory inventory = player.GetComponent<Inventory>();

            if (inventory != null)
            {
                inventory.AddItem(newItem);
                
            }

            this.gameObject.SetActive(false);

        }
    }
}
