using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogData : MonoBehaviour
{
  internal List<DialogLine> Lines;



  internal class DialogLine
  {
    public string UID;
    public string SequenceID;
    public string Dialog;
  }
}
