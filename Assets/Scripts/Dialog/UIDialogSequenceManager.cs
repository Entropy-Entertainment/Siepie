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
  public void StartUIDialogSequence(string speakerLeft, string speakerRight)
  {
    UI.rootVisualElement.style.display = DisplayStyle.Flex;
    this.speakerLeft.text = speakerLeft;
    this.speakerRight.text = speakerRight;
  }

  /// <summary>
  /// Update the UI dialog box with the provided parameters and start text-typing coroutines.
  /// </summary>
  /// <param name="speakerName">Name of the speaking character to display and highlight.</param>
  /// <param name="dialogText">The dialog text to be displayed in the UI.</param>
  internal void UpdateUI(string speakerName, string dialogText)
  {
    setSpeakerColor(speakerName);
    StartCoroutine(TypeText(this.currentSpeaker, speakerName));
    StartCoroutine(TypeText(dialogTextDisplay, dialogText));
  }
  /// <summary>
  /// Animate text into a <see cref="Label"/> one character at a time.
  /// </summary>
  /// <param name="target">The label that will receive the typed text.</param>
  /// <param name="fullText">The complete text to type out.</param>
  /// <param name="timer">Delay in seconds between each character (defaults to 0.01s).</param>
  /// <returns>Coroutine enumerator suitable for <see cref="MonoBehaviour.StartCoroutine"/>.</returns>
  public IEnumerator TypeText(Label target, string fullText, float timer = 0.01f)
  {
    target.text = "";
    foreach (char c in fullText)
    {
      target.text += c;
      yield return new WaitForSeconds(timer);
    }
  }

  /// <summary>
  /// Update speaker images' tint to visually highlight the active speaker.
  /// </summary>
  /// <param name="speakerName">Name of the currently speaking character.</param>
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
