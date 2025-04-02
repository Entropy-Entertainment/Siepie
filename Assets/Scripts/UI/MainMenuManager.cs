using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public Transform imageMove;
    public GameObject image;
    public float LerpSpeed;

    private void FixedUpdate()
    {
        if (Vector3.Lerp(image.transform.position, imageMove.position, LerpSpeed) != imageMove.position)
        {
            Debug.Log("Lerping");
            image.transform.position = Vector3.Lerp(image.transform.position, imageMove.position, LerpSpeed);
        }
    }
}
