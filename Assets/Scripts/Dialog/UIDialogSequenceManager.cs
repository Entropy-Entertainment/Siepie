using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// The class that manages the UI prefab DialogTemplate.
/// This class must link to an <see cref="INpcDialog"/> object to receive dialog updates.
/// It queries the UIDocument for the expected <see cref="Label"/> elements and updates
/// them when the linked NPC raises its <c>UpdateDialog</c> event.
/// </summary>
public class UIDialogSequenceManager : MonoBehaviour
{ 
  [SerializeField] UIDocument UI;
  Label currentSpeaker;
  Label dialogTextDisplay;
  Label speakerLeft;
  Label speakerRight;
  INpcDialog npcDialogProvider;

  /// <summary>
  /// Queries the provided <see cref="UIDocument"/> for the
  /// dialog-related <see cref="Label"/> elements by name and caches references to them.
  /// If the expected elements are not present the fields will remain null and
  /// <see cref="updateUI(string,string,string)"/> will skip updates for missing elements.
  /// </summary>
  void Start()
  {
    currentSpeaker = UI.rootVisualElement.Q<Label>("CurrentlySpeaking");
    dialogTextDisplay = UI.rootVisualElement.Q<Label>("Text");
    speakerLeft = UI.rootVisualElement.Q<Label>("SpeakerLeft");
    speakerRight = UI.rootVisualElement.Q<Label>("SpeakerRight");
  }

  /// <summary>
  /// Subscribes to the provided NPC's <c>UpdateDialog</c> event to set the initial
  /// dialog UI values (left/right speaker names and initial text). The temporary
  /// handler unsubscribes itself after it runs once to avoid handling future updates.
  /// After applying the initial values this method also links the persistent update handler
  /// via <see cref="LinkUpdateUIEvent(INpcDialog)"/>.
  /// </summary>
  /// <param name="npcDialogObj">The NPC providing dialog updates; must implement <see cref="INpcDialog"/>.</param>
  public void StartUIDialogSequence(INpcDialog npcDialogObj)
  {
    npcDialogObj.UpdateDialog += SetDialogFirstTime;

    void SetDialogFirstTime(string speakerLeftName, string speakerRightName, string initialText)
    {
      speakerLeft.text = speakerLeftName;
      speakerRight.text = speakerRightName;
      dialogTextDisplay.text = initialText;
      dialogTextDisplay.style.display = DisplayStyle.None;
      npcDialogObj.UpdateDialog -= SetDialogFirstTime;
    }
    LinkUpdateUIEvent(npcDialogObj);
  }

  /// <summary>
  /// Links this manager to the provided <see cref="INpcDialog"/>'s <c>UpdateDialog</c> event.
  /// If another provider is already linked it will be unlinked first. The manager stores
  /// a reference to the provider so it can unsubscribe later via <see cref="UnlinkUpdateUIEvent"/>.
  /// </summary>
  /// <param name="npcDialogObj">The npc from which this class will receive updates.</param>
  internal void LinkUpdateUIEvent(INpcDialog npcDialogObj)
  {
    UnlinkUpdateUIEvent();
    npcDialogProvider = npcDialogObj;
    npcDialogProvider.UpdateDialog += updateUI;
  }

  /// <summary>
  /// Updated the UI dialog box with the provided parameters.
  /// This method is intended to be used as an event handler for <see cref="INpcDialog.UpdateDialog"/>.
  /// </summary>
  /// <param name="speakerName">The character on the left of the dialog box.</param>
  /// <param name="listener">The npc with whom the player interacted.</param>
  /// <param name="dialogText">The dialog to be displayed in the UXML UI.</param>
  void updateUI(string speakerName, string listener, string dialogText)
  {
    if (currentSpeaker != null)
      currentSpeaker.text = speakerName;

    if (dialogTextDisplay != null)
      dialogTextDisplay.text = dialogText;
  }

  /// <summary>
  /// Unlinks from the currently stored <see cref="INpcDialog"/> provider, if any,
  /// by unsubscribing the persistent <see cref="updateUI(string,string,string)"/> handler.
  /// This should be called when the manager is destroyed or when switching to a different NPC
  /// to avoid leaked subscriptions or callbacks to destroyed objects.
  /// </summary>
  void UnlinkUpdateUIEvent()
  {
    if (npcDialogProvider != null)
    {
      npcDialogProvider.UpdateDialog -= updateUI;
      npcDialogProvider = null;
    }
  }
}
