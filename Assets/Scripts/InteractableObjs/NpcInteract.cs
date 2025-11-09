using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Player.Interaction
{
  /// <summary>
  /// Component placed on NPC game objects that makes them interactable and exposes
  /// dialog via the <see cref="UpdateDialog"/> event. When the player interacts
  /// this component will create a UI dialog prefab and link it to this NPC so the
  /// UI can receive dialog updates.
  /// </summary>
  public class NpcInteract : MonoBehaviour, IInteractable, INpcDialog
  {
    /// <summary>
    /// Event raised when the NPC has dialog data to present. Subscribers (typically
    /// a UI manager) should handle the parameters: left speaker, right speaker, and text.
    /// </summary>
    public event Action<string, string, string> UpdateDialog;

    /// <summary>
    /// Array of dialog UIDs that are relevant for this NPC. Used to filter global
    /// dialog data to the subset this NPC should present.
    /// </summary>
    [SerializeField] internal int[] dialogUID;

    /// <summary>
    /// Shared deserializer instance used to load dialog JSON. Initialized from
    /// <see cref="InitDialogSystem.dialogDeserializer"/> in <see cref="Start"/>.
    /// </summary>
    static internal Deserializer<DialogData> dialogDeserializer;

    /// <summary>
    /// Cached dialog lines for this NPC, filtered from the global dialog data by <see cref="dialogUID"/>.
    /// </summary>
    DialogData.DialogLine[] dialogLines;

    /// <summary>
    /// Prefab for the UI dialog. Instantiated when the player interacts.
    /// </summary>
    [SerializeField] GameObject UIDialogPrefab;

    /// <summary>
    /// The instantiated dialog manager used to drive the UI for this NPC interaction.
    /// </summary>
    UIDialogSequenceManager UIDialogSequenceManager;

    int currentSequence;
    
    void Start()
    {
      dialogDeserializer = InitDialogSystem.dialogDeserializer;
      SubscribeToInteractEvent();
    }

    /// <summary>
    /// Handler invoked when the player interacts. If the interacted object is this NPC,
    /// the method prepares dialog lines, instantiates the UI prefab and links the UI to this NPC
    /// so the UI can receive updates via <see cref="UpdateDialog"/>.
    /// </summary>
    /// <param name="player">The player GameObject performing the interaction.</param>
    /// <param name="interactedObject">The GameObject that was interacted with.</param>
    public void PlayerInteracted(GameObject player, GameObject interactedObject)
    {
      if (interactedObject == this.gameObject)
      {
        GetAllRelevantUID();
        UIDialogSequenceManager = Instantiate(UIDialogPrefab).GetComponent<UIDialogSequenceManager>();
        UIDialogSequenceManager.LinkUpdateUIEvent(this);
      }
    }

    /// <summary>
    /// Filters the globally-deserialized dialog data to the subset that matches this NPC's
    /// <see cref="dialogUID"/> list and caches the resulting lines in <see cref="dialogLines"/>.
    /// </summary>
    internal void GetAllRelevantUID()
    {
      dialogLines = dialogDeserializer.GetDeserializedObject().Lines
        .Where(aDialogLine => dialogUID.Contains(aDialogLine.UID))
        .ToArray();
      print(dialogLines.Length);
    }

    /// <summary>
    /// Subscribes this NPC to the player interactor's static event so <see cref="PlayerInteracted"/>
    /// will be called when the player interacts. Complemented by unsubscription in <see cref="OnDestroy"/>.
    /// </summary>
    public void SubscribeToInteractEvent()
    {
      PlayerInteractor.PlayerInteract += PlayerInteracted;
    }

    /// <summary>
    /// Unity OnDestroy callback. Unsubscribes from the player interactor event to avoid
    /// leaving stale handlers when the NPC is destroyed.
    /// </summary>
    void OnDestroy()
    {
      PlayerInteractor.PlayerInteract -= PlayerInteracted;
    }
  }
}