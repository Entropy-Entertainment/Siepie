using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Initializes the dialog system for the game. This component ensures the dialog
/// deserializer is created once at startup and preserved across scene loads.
/// </summary>
[DefaultExecutionOrder(-100)]
public class InitDialogSystem : MonoBehaviour
{
  /// <summary>
  /// Singleton-like access to the application's <see cref="DialogDeserializer"/>.
  /// The instance is created during <see cref="Start"/> and is kept across scenes
  /// via <see cref="Object.DontDestroyOnLoad"/>.
  /// </summary>
  public static DialogDeserializer dialogDeserializer { get; private set; }

  void Start()
  {
    DontDestroyOnLoad(this.gameObject);
    dialogDeserializer = new DialogDeserializer($"DialogData/{SceneManager.GetActiveScene().name}");
    dialogDeserializer.ResourcesAPILoader();
    if (dialogDeserializer.GetDeserializedObject() == null)
    {
      Debug.LogWarning("Unable to deserialize the json from the starting scene");
    }
  }
}