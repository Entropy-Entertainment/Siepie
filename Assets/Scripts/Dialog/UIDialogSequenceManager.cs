using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// The class that manages the UI prefab DialogTemplate.
/// This class must link to an INpcDialog object to receive dialog updates.
/// </summary>
public class UIDialogSequenceManager : MonoBehaviour
{
  [SerializeField] UIDocument UI;
  Label currentSpeaker;
  Label dialogTextDisplay;
  Label speakerLeft;
  Label speakerRight;
  INpcDialog npcDialogProvider;

  void Start()
  {
    currentSpeaker = UI.rootVisualElement.Q<Label>("CurrentlySpeaking");
    dialogTextDisplay = UI.rootVisualElement.Q<Label>("Text");
    speakerLeft = UI.rootVisualElement.Q<Label>("SpeakerLeft");
    speakerRight = UI.rootVisualElement.Q<Label>("SpeakerRight");
  }

  /// <summary>
  /// Make sure to invoke UpdateDialog event from the INpcDialog object after calling this method to set the initial dialog values.
  /// </summary>
  /// <param name="npcDialogObj">The npc from which this class will recieve updates</param>
  public void StartUIDialogSequence(string speakerLeft, string speakerRight, string initialText)
  {
    this.speakerLeft.text = speakerLeft;
    this.speakerRight.text = speakerRight;
    dialogTextDisplay.text = initialText;
    dialogTextDisplay.style.display = DisplayStyle.None;
  }

  /// <summary>
  /// Updated the UI dialog box with the provided parameters.
  /// </summary>
  /// <param name="speakerName">The character on the left of the dialog box</param>
  /// <param name="listener">The npc with whom the player interacted with (unless the parameters get modified in the NpcInteract class) </param>
  /// <param name="dialogText">The dialog to be displayed in the uxml UI</param>
  internal void UpdateUI(string speakerName, string dialogText)
  {
    if (currentSpeaker != null)
      currentSpeaker.text = speakerName;

    if (dialogTextDisplay != null)
      dialogTextDisplay.text = dialogText;
  }

  void UnlinkUpdateUIEvent()
  {
    if (npcDialogProvider != null)
    {
      npcDialogProvider.UpdateDialog -= updateUI;
      npcDialogProvider = null;
    }
  }
}
