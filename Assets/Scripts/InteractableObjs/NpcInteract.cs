using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.Interaction
{
  public class NpcInteract : MonoBehaviour, IInteractable
  {
    [SerializeField] internal int[] dialogUID;
    DialogData.DialogLine[] dialogLines;
    static internal DialogDeserializer dialogDeserializer = new("DialogData");
    void Start()
    {
      SubscribeToInteractEvent();
      getAllRelevantUID();
    }

    public void PlayerInteracted(GameObject player, GameObject interactedObject)
    {
      throw new System.NotImplementedException();
    }

    void getAllRelevantUID()
    {
      dialogLines = dialogDeserializer.GetDeserializedObject().Lines
        .Where(aDialogLine => dialogUID.Contains(aDialogLine.UID))
        .ToArray();
    }

    public void SubscribeToInteractEvent()
    {
      PlayerInteractor.PlayerInteract += PlayerInteracted;
    }
  }
}