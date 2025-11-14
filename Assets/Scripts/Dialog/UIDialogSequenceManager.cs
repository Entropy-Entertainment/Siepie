using System.Collections;
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
  Image speakerLeftImage;
  Image speakerRightImage;


  void Start()
  {
    speakerLeftImage = UI.rootVisualElement.Q<Image>("Person1");
    speakerRightImage = UI.rootVisualElement.Q<Image>("Person2");
    currentSpeaker = UI.rootVisualElement.Q<Label>("CurrentlySpeaking");
    dialogTextDisplay = UI.rootVisualElement.Q<Label>("Text");
    speakerLeft = UI.rootVisualElement.Q<Label>("SpeakerLeft");
    speakerRight = UI.rootVisualElement.Q<Label>("SpeakerRight");
    UI.rootVisualElement.style.display = DisplayStyle.None;
  }

  /// <summary>
  /// Make sure to invoke UpdateDialog event from the INpcDialog object after calling this method to set the initial dialog values.
  /// </summary>
  /// <param name="npcDialogObj">The npc from which this class will recieve updates</param>
  public void StartUIDialogSequence(string speakerLeft, string speakerRight, string initialText, string currentSpeaker)
  {
    UI.rootVisualElement.style.display = DisplayStyle.Flex;
    this.speakerLeft.text = speakerLeft;
    this.speakerRight.text = speakerRight;
  }

  /// <summary>
  /// Updated the UI dialog box with the provided parameters.
  /// </summary>
  /// <param name="speakerName">The character on the left of the dialog box</param>
  /// <param name="listener">The npc with whom the player interacted with (unless the parameters get modified in the NpcInteract class) </param>
  /// <param name="dialogText">The dialog to be displayed in the uxml UI</param>
  internal void UpdateUI(string speakerName, string dialogText)
  {
    setSpeakerColor(speakerName);
    StartCoroutine(TypeText(this.currentSpeaker, speakerName));
    StartCoroutine(TypeText(dialogTextDisplay, dialogText));
  }

  public IEnumerator TypeText(Label target, string fullText, float timer = 0.01f)
  {
    target.text = "";
    foreach (char c in fullText)
    {
      target.text += c;
      yield return new WaitForSeconds(timer);
    }
  }

  void setSpeakerColor(string speakerName)
  {
    if (speakerName == this.speakerLeft.text)
    {
      speakerRightImage.style.unityBackgroundImageTintColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
      speakerLeftImage.style.unityBackgroundImageTintColor = new Color(1f, 1f, 1f, 1f);
    }
    else
    {
      speakerRightImage.style.unityBackgroundImageTintColor = new Color(1f, 1f, 1f, 1f);
      speakerLeftImage.style.unityBackgroundImageTintColor = new Color(0.5f, 0.5f, 0.5f, 0.5f);
    }
  }
}
