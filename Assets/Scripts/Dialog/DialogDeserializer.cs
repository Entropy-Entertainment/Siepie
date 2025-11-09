using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Deserializes dialog data from JSON files located in the Resources/DialogData/ directory.
/// LoadResoucesAPI() must be called for the first scene's loading.
/// Listens for scene changes to load the appropriate dialog data for the new scene.
/// Should work completely automatically after initialization in InitDialogSystem.
/// </summary>
public class DialogDeserializer : Deserializer<DialogData>
{
  /// <summary>
  /// Gets the file path used when loading dialog JSON data from Resources.
  /// This value is initialized to "DialogData/" and appended with the scene name when
  /// a scene change occurs.
  /// </summary>
  public string DialogFilePath { get => dialogFilePath; private set => dialogFilePath = value; }
  string dialogFilePath = "DialogData/";

  /// <summary>
  /// Creates a new <see cref="DialogDeserializer"/> and subscribes to scene change events.
  /// </summary>
  /// <param name="resourcesApiPath">The base Resources path used by the underlying deserializer.
  /// Typically this is something like "DialogData/&lt;SceneName&gt;".</param>
  public DialogDeserializer(string resourcesApiPath) : base(resourcesApiPath)
  {
    SceneManager.activeSceneChanged += LoadNewSceneDialog;
    Debug.Log($"DialogDeserializer initialized with path: {resourcesApiPath}");
  }

  /// <summary>
  /// Handler invoked when the active scene changes. This method will update the internal
  /// dialog file path to target the new scene, call the resources loader and attempt to
  /// deserialize the JSON file for the new scene.
  /// </summary>
  /// <param name="current">The scene that was active before the change.</param>
  /// <param name="next">The scene that is now active.</param>
  /// <remarks>
  /// This method updates <see cref="DialogFilePath"/> by appending the new scene name,
  /// calls <see cref="ResourcesAPILoader()"/>, and if a JSON file was loaded will call
  /// <see cref="GetDeserializedObject()"/> to populate the deserialized object.
  /// </remarks>
  void LoadNewSceneDialog(Scene current, Scene next)
  {
    Debug.Log($"Scene changed from {current.name} to {next.name}");
    dialogFilePath = resourcesApiPath = dialogFilePath + next.name;

    ResourcesAPILoader();
    if (jsonTextFile != null)
    {
      GetDeserializedObject();
    }
  }
}
