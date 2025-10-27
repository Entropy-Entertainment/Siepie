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
    static internal Deserializer<DialogData> dialogDeserializer = new("DialogData");
    void Start()
    {
      SubscribeToInteractEvent();
    }

    public void PlayerInteracted(GameObject player, GameObject interactedObject)
    {
      if (interactedObject == this.gameObject) currentSequence++; 
    }

    internal void GetAllRelevantUID()
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

    void OnDestroy()
    {
      PlayerInteractor.PlayerInteract -= PlayerInteracted;
    }
  }
}