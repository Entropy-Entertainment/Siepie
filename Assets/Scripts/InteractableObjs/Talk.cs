using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using TMPro;

public class Talk : PlayerInteraction
{
    public TextMeshPro text;

    //I call my potato a structure the way it's so structurally sound -Henry
    public struct Potato
    {
        public static bool has = false;
        public static bool hasgiven = false;
    }
   
    //GIVE ME A DIALOGUE SYSTEM NORA AND MY LIFE IS YOURS -Henry
    public override void PlayerInteracted(GameObject player, GameObject interactedObject)
    {
        if(interactedObject == this.gameObject)
        {
            Inventory inventory = player.GetComponent<Inventory>();

            Potato.has = inventory.HasItemID(1);

            if (player.CompareTag("Cat") && Potato.hasgiven == false)
            {
                text.text = 
               ("Hello there good fellow, could you give me a potato?");
            }
            else if(player.CompareTag("Cat") && Potato.hasgiven == true)
            {
                text.text =("Jolly good chum! I'm forever in your debt");
            }
            else if (player.CompareTag("Human") && Potato.has == false)
            {
                text.text =("SCRAAAAAAAAW");
            }
            else
            {
                text.text =("(Happy) SCRAAAAAW");
                Potato.hasgiven = true;
                inventory.RemoveItemByID(1);
            }
        }
    }
}
