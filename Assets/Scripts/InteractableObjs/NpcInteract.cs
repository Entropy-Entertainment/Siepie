using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.Interaction
{
  public class NpcInteract : MonoBehaviour, IInteractable
  {
    [SerializeField] internal int[] dialogUID;
    DialogData.DialogLine[] dialogLines;
    int currentSequence;
    static internal DialogDeserializer dialogDeserializer = new("DialogData");
    void Start()
    {
      SubscribeToInteractEvent();
      getAllRelevantUID();
    }

    public void PlayerInteracted(GameObject player, GameObject interactedObject)
    {
      if (interactedObject == this.gameObject) currentSequence++; 
    }

    void getAllRelevantUID()
    {
      dialogLines = dialogDeserializer.GetDeserializedObject().Lines
        .Where(aDialogLine => dialogUID.Contains(aDialogLine.UID))
        .ToArray();
      print(dialogLines.Length);
    }

    public void SubscribeToInteractEvent()
    {
      PlayerInteractor.PlayerInteract += PlayerInteracted;
    }
  }
}