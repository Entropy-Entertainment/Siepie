using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class DialogDeserializer_CurrentScene
{
  DialogDeserializer dialogDeserializer;
  Scene currentScene;
  [UnitySetUp]
  public IEnumerator UnitySetUp()
  {
    SceneManager.LoadScene("Stockholm");
    yield return null;
    currentScene = SceneManager.GetActiveScene();
    dialogDeserializer = new DialogDeserializer($"DialogData/{currentScene.name}");
    dialogDeserializer.ResourcesAPILoader();
  }

  [UnityTest]
  public IEnumerator DialogDeserializer_GetCurrentSceneDialog()
  {
    // Arrange
    yield return null;
    // Act Assert
    Assert.AreEqual(dialogDeserializer.GetCurrentlyAssignedJson().name.ToLower(), currentScene.name.ToLower(), $"No {typeof(DialogData)} for this scene");
  }
}
