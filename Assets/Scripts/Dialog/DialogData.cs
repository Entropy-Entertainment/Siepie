using System;

[Serializable]
public class DialogData
{
  public DialogLine[] Lines;
  [Serializable] public class DialogLine
  {
    public int UID;
    public int SequenceID;
    public string Dialog;
  }
}
