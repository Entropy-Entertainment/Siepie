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
  public string DialogFilePath { get => dialogFilePath; private set => dialogFilePath = value; }
  string dialogFilePath = "DialogData/";

  public DialogDeserializer(string resourcesApiPath) : base(resourcesApiPath)
  {
    SceneManager.activeSceneChanged += LoadNewSceneDialog;
    Debug.Log($"DialogDeserializer initialized with path: {resourcesApiPath}");
  }

  void LoadNewSceneDialog(Scene current, Scene next)
  {
    Debug.Log($"Scene changed from {current.name} to {next.name}");
    dialogFilePath = resourcesApiPath = dialogFilePath + next.name;

    ResourcesAPILoader();
    if(jsonTextFile != null)
    {
      GetDeserializedObject();
    }
  }
}
