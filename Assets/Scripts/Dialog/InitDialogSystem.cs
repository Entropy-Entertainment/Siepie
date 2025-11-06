using UnityEngine;
using UnityEngine.SceneManagement;

public class InitDialogSystem : MonoBehaviour
{
  public static DialogDeserializer dialogDeserializer { get; private set; }
  void Start()
  {
    DontDestroyOnLoad(this.gameObject);
    dialogDeserializer = new DialogDeserializer($"DialogData/{SceneManager.GetActiveScene().name}");
  }
}