using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-100)]
public class InitDialogSystem : MonoBehaviour
{
  [SerializeField] public static string DialogFilePath = "DialogData"; //Dont end the path with a "/" 
  public static Deserializer<DialogData> DialogDeserializer { get; private set; }
  void Start()
  {
    DontDestroyOnLoad(this.gameObject);
    SetDeserializer(new Deserializer<DialogData>(DialogFilePath, SceneManager.GetActiveScene().name));
    SceneManager.activeSceneChanged += LoadNewSceneDialog;
  }

  public void SetDeserializer(Deserializer<DialogData> dialogDeserializer)
  {
    DialogDeserializer = dialogDeserializer;
    DialogDeserializer.ResourcesAPILoader();
    DialogDeserializer.GetDeserializedObject();
  }

  void LoadNewSceneDialog(Scene current, Scene next)
  {
    DialogDeserializer.SceneFileName = next.name;
    DialogDeserializer.ResourcesAPILoader();
    DialogDeserializer.GetDeserializedObject();
  }
}