using NSubstitute;
using Player.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NpcInteract))]

public class NpcDialogPreparer : MonoBehaviour
{
  [SerializeField] internal int[] dialogUID;
  [SerializeField] GameObject UIDialogPrefab;
  Deserializer<DialogData> dialogDeserializer;
  DialogData.DialogLine[] dialogLines;
  
  UIDialogSequenceManager UIDialogSequenceManager;
  int currentSequence = 0;
  void Start()
  {
    dialogDeserializer = InitDialogSystem.dialogDeserializer;
    GetComponent<NpcInteract>().WasInteractedWith += dialogIsRequested;
    dialogLines = GetAllRelevantUID();
    UIDialogSequenceManager = Instantiate(UIDialogPrefab).GetComponent<UIDialogSequenceManager>();
  }

  void dialogIsRequested(GameObject player, GameObject thisNpc)
  {
    if (currentSequence == 0)
    {
      var speakers = dialogLines.Select(x => x.Speaker).Distinct().ToList();
      speakers.RemoveAll(s => s == "PLAYER");

      UIDialogSequenceManager.StartUIDialogSequence(player.name, speakers.First(), dialogLines[currentSequence].Dialog);
    }
    var currentLine = dialogLines.Where(sq => sq.SequenceID == currentSequence).First();
    UIDialogSequenceManager.UpdateUI(currentLine.Speaker, currentLine.Dialog);
    currentSequence++;
  }

  DialogData.DialogLine[] GetAllRelevantUID()
  {
    return dialogDeserializer.GetDeserializedObject().Lines
      .Where(aDialogLine => dialogUID.Contains(aDialogLine.UID))
      .ToArray();
  }

  private void OnDestroy()
  {
    GetComponent<NpcInteract>().WasInteractedWith -= dialogIsRequested;
  }
}
