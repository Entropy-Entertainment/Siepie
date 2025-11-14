using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogDeserializer : Deserializer<DialogData>
{
  public string DialogFilePath { get => dialogFilePath; private set => dialogFilePath = value; }
  string dialogFilePath = "DialogData/";

  public DialogDeserializer(string resourcesApiPath) : base(resourcesApiPath)
  {
    SceneManager.activeSceneChanged += LoadNewSceneDialog;
    Debug.Log($"DialogDeserializer initialized with path: {resourcesApiPath}");
    Debug.Log($"Current active scene: {SceneManager.GetActiveScene().name}");
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
