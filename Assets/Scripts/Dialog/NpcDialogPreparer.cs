using Player.Interaction;
using System;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(NpcInteract))]

public class NpcDialogPreparer : MonoBehaviour
{
  [SerializeField] internal int[] dialogUID;
  [SerializeField] GameObject UIDialogPrefab;
  Deserializer<DialogData> DialogDeserializer;
  DialogData.DialogLine[] dialogLines;
  UIDialogSequenceManager UIDialogSequenceManager;
  int currentSequence = 0;
  void Start()
  {
    DialogDeserializer = InitDialogSystem.DialogDeserializer;
    GetComponent<NpcInteract>().WasInteractedWith += dialogIsRequested;
    dialogLines = GetAllRelevantUID();
    var dialogObj = Instantiate(UIDialogPrefab);
    UIDialogSequenceManager = dialogObj.GetComponent<UIDialogSequenceManager>();
  }

  void dialogIsRequested(GameObject player, GameObject thisNpc)
  {
    print("sequence");
    if (currentSequence == 0)
    {
      var speakers = dialogLines.Select(x => x.Speaker).Distinct().ToList();
      speakers.RemoveAll(s => s == "PLAYER");

      UIDialogSequenceManager.StartUIDialogSequence(player.name, speakers.First());
    }
    var currentLine = dialogLines.Where(sq => sq.SequenceID == currentSequence).First();
    if (currentLine.Speaker == "PLAYER") currentLine.Speaker = player.name;
    UIDialogSequenceManager.UpdateUI(currentLine.Speaker, currentLine.Dialog);
    currentSequence++;
  }

  DialogData.DialogLine[] GetAllRelevantUID()
  {
    print(DialogDeserializer.GetDeserializedObject().Lines.Length);
    return DialogDeserializer.GetDeserializedObject().Lines
      .Where(aDialogLine => dialogUID.Contains(aDialogLine.UID))
      .ToArray();
  }

  private void OnDestroy()
  {
    GetComponent<NpcInteract>().WasInteractedWith -= dialogIsRequested;
  }
}
