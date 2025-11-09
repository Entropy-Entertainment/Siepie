using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.Interaction
{
  public class NpcInteract : MonoBehaviour, IInteractable, INpcDialog
  {
    public event Action<string, string, string> UpdateDialog;
    [SerializeField] internal int[] dialogUID;
    static internal Deserializer<DialogData> dialogDeserializer;
    DialogData.DialogLine[] dialogLines;
    [SerializeField] GameObject UIDialogPrefab;
    UIDialogSequenceManager UIDialogSequenceManager;
    int currentSequence;
   
    void Start()
    {
      dialogDeserializer = InitDialogSystem.dialogDeserializer;
      SubscribeToInteractEvent();
    }

    public void PlayerInteracted(GameObject player, GameObject interactedObject)
    {
      if (interactedObject == this.gameObject)
      {
        GetAllRelevantUID();
        UIDialogSequenceManager = Instantiate(UIDialogPrefab).GetComponent<UIDialogSequenceManager>();
        UIDialogSequenceManager.LinkUpdateUIEvent(this);
      }
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