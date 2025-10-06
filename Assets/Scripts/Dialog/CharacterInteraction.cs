using Player.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Player.Interaction
{
  public class CharacterInteraction : PlayerInteractor
  {
    public List<NpcInteract> NpcList { get; private set; } = new();

    private void Start()
    {
      base.Start();
    }
    internal void getAllNpcsWithInteractableTag()
    {
      NpcList.AddRange(
          InteractableObjects
              .Select(x => (x as MonoBehaviour)?.gameObject.GetComponent<NpcInteract>())
              .Where(npc => npc != null)
      );
    }

    internal void checkDistanceofNpcsToPlayer(Transform playerTransform, float interactionRange)
    {
      foreach (var npc in NpcList)
      {
        float distance = Vector3.Distance(playerTransform.position, npc.transform.position);
        if (distance <= interactionRange)
        {
          // Debug.Log($"NPC {npc.name} is within interaction range.");
          // You can add logic here to enable interaction prompts or highlight the NPC
        }
        else
        {
          // Debug.Log($"NPC {npc.name} is out of interaction range.");
          // You can add logic here to disable interaction prompts or remove highlights
        }
      }
    }
  }
}