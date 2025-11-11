using Player.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NpcInteract))]

public class NpcDialogPreparer : MonoBehaviour, INpcDialog
{
  /// <summary>
  /// First string: Speaker name (left side of dialog box)
  /// Second string: Listener name (right side of dialog box)
  /// Thrid string: Dialog text
  /// </summary>
  public event Action<string, string, string> UpdateDialog;
  public DialogData.DialogLine currentDialogLine { get; private set; }
  [SerializeField] internal int[] dialogUID;
  static internal Deserializer<DialogData> dialogDeserializer;
  DialogData.DialogLine[] dialogLines;
  [SerializeField] GameObject UIDialogPrefab;
  UIDialogSequenceManager UIDialogSequenceManager;
  int currentSequence;
  void Start()
  {
    dialogDeserializer = InitDialogSystem.dialogDeserializer;
    GetComponent<NpcInteract>().WasInteractedWith += dialogIsRequested;
  }

  void dialogIsRequested(GameObject player, GameObject thisNpc)
  {
    currentSequence = 0;
    GetAllRelevantUID();
    UIDialogSequenceManager = Instantiate(UIDialogPrefab).GetComponent<UIDialogSequenceManager>();
    UIDialogSequenceManager.StartUIDialogSequence(this);

    var currentLine = serveDataForCurrentLine(dialogLines.Where(sq => sq.SequenceID == currentSequence).First(), player);
    UpdateDialog?.Invoke(currentLine.speakerLeft, currentLine.speakerRight, currentLine.dialogText);
  }

  internal void GetAllRelevantUID()
  {
    dialogLines = dialogDeserializer.GetDeserializedObject().Lines
      .Where(aDialogLine => dialogUID.Contains(aDialogLine.UID))
      .ToArray();
    print(dialogLines.Length);
  }

  (string speakerLeft, string speakerRight, string dialogText) serveDataForCurrentLine(DialogData.DialogLine currentLine, GameObject player)
  {
    return (currentLine.Speaker, "Player", currentLine.Dialog);
  }

  void Update()
  {

  }
}
