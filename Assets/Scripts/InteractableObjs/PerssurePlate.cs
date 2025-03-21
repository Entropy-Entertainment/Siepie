using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Persure : MonoBehaviour
{

    public UnityEvent Enter,Exit;


    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.tag == "Human")
        {
            Enter.Invoke();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Human")
        {
            Exit.Invoke();
        }
    }

    public void Test(string test)
    {
        Debug.Log(test);
    }
}
