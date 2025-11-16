using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class InitDialogSystem_SwitchScene
{
  Deserializer<DialogData> dialogDeserializer;
  string currentScene;

  [UnitySetUp]
  public IEnumerator UnitySetUp()
  {
    yield return SceneManager.LoadSceneAsync("Stockholm");
    yield return null;
    currentScene = SceneManager.GetActiveScene().name;
    dialogDeserializer = new Deserializer<DialogData>("DialogData", currentScene);
  }
  [UnityTest]
  public IEnumerator InitDialogSystem_CheckSceneChangeForCorrectName()
  {
    //Arrange
    var nextScene = "GeneralTestScene";
    yield return null;
    // Act
    SceneManager.LoadScene(nextScene);
    yield return null;
    // Assert
    Assert.AreEqual(dialogDeserializer.SceneFileName.ToLower().Replace("dialogdata/", ""), nextScene.ToLower(), $"No {typeof(DialogData)} for this scene");
  }
}
