using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class DialogDeserializer_SwitchScene
{
  DialogDeserializer dialogDeserializer;
  string currentScene;

  [UnitySetUp]
  public IEnumerator UnitySetUp()
  {
    yield return SceneManager.LoadSceneAsync("Stockholm");
    yield return null;
    currentScene = SceneManager.GetActiveScene().name;
    dialogDeserializer = new DialogDeserializer($"DialogData/{currentScene}");
  }
  [UnityTest]
  public IEnumerator DialogDeserializer_CheckSceneChangeForCorrectDialog()
  {
    //Arrange
    var nextScene = "GeneralTestScene";
    yield return null;
    // Act
    SceneManager.LoadScene(nextScene);
    yield return null;
    // Assert
    Assert.AreEqual(dialogDeserializer.DialogFilePath.ToLower().Replace("dialogdata/", ""), nextScene.ToLower(), $"No {typeof(DialogData)} for this scene");
  }
}
