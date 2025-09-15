using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class DialogData_Deserialize
{
  [Test]
  public void DialogData_CanDeserializeUID()
  {
    //Act
    var dialogData = JsonUtility.FromJson<DialogData>(@"{
      ""lines"": [
        {
          ""UID"": ""1"",
          ""SequenceID"": ""5"",
          ""Dialog"": ""Hi there"",
        },
        {
          ""UID"": ""Welcome to the game."",
          ""speakerName"": ""Guide"",
          ""portraitImagePath"": ""Assets/Images/Guide.png"",
        }
      ]
    }");

    //Assert
    Assert.IsNotNull(dialogData.Lines[0].UID);
  }


}
