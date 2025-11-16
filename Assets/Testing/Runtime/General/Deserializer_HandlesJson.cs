using NUnit.Framework;
using System;
using System.Diagnostics;
using UnityEngine.SceneManagement;

public class Deserializer_HandlesJson
{
  string jsonResourceAPIpath = "DialogData";

  [TestCase(typeof(DialogData))]
  public void Deserializer_GivesTypeBackBasedOnClassGeneric(Type type)
  {
    //Arrange
    Type expectedType = type;
    Type actualType;
    Deserializer<DialogData> deserializer = new(jsonResourceAPIpath, SceneManager.GetActiveScene().name);
    deserializer.ResourcesAPILoader();
    var jsonTextFile = deserializer.GetCurrentlyAssignedJson();

    //Act
    actualType = deserializer.GetDeserializedObject().GetType();

    //Assert
    Assert.AreEqual(expectedType, actualType);
  }

  [TestCase(typeof(DialogData))]
  public void Deserializer_CanGiveObjectBackFromValidJson(Type type)
  {
    //Arrange
    Deserializer<DialogData> deserializer = new(jsonResourceAPIpath, SceneManager.GetActiveScene().name);
    deserializer.ResourcesAPILoader();
    var jsonTextFile = deserializer.GetCurrentlyAssignedJson();
    //Act
    var deserializedObject = deserializer.GetDeserializedObject();
    //Assert
    Assert.IsNotNull(deserializedObject);
    Debug.Print(deserializedObject.ToString());
  }
}
