using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class InitDialogSystem : MonoBehaviour
{
  public static DialogDeserializer dialogDeserializer { get; private set; }
  void Start()
  {
    DontDestroyOnLoad(this.gameObject);
    dialogDeserializer = new DialogDeserializer($"DialogData/{SceneManager.GetActiveScene().name}");
    dialogDeserializer.ResourcesAPILoader();
    if(dialogDeserializer.GetDeserializedObject() == null)
    {
      Debug.LogWarning("Couldnt deserialize the json from the starting scene");
    }
  }
}