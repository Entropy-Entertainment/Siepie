using NUnit.Framework;
using UnityEngine;


public class DialogData_Deserialize
{
  DialogData dialogData = new();
  [SetUp]
  public void Setup()
  {
    //Arrange and Act
    string text = Resources.Load<TextAsset>("DialogData/Stockholm").text;
    dialogData = JsonUtility.FromJson<DialogData>(text);
  }

  [Test] public void DialogData_DeserializeList()
  {
    Assert.AreNotSame(dialogData.Lines.Length, 0);
  }


  [Test]
  public void DialogData_CanDeserializeUID()
  {
    //Assert
    Assert.IsNotNull(dialogData.Lines[0].UID);
  }
  [Test]
  public void DialogData_CanDeserializeSequenceID([NUnit.Framework.Range(0, 1, 1)] int index)
  {
    //Assert
    Assert.IsNotNull(dialogData.Lines[index].SequenceID);
  }
  [Test]
  public void DialogData_CanDeserializeDialog([NUnit.Framework.Range(0, 1, 1)] int index)
  {
    //Assert
    Assert.IsNotNull(dialogData.Lines[index].Dialog);
  }
}
