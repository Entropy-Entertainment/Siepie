using UnityEngine;
using UnityEngine.SceneManagement;

public class InitDialogSystem : MonoBehaviour
{
  public static DialogDeserializer dialogDeserializer { get; private set; }
  void Start()
  {
    dialogDeserializer = new DialogDeserializer($"DialogData/{SceneManager.GetActiveScene().name}");
  }
}
