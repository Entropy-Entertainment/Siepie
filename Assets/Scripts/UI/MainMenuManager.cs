using Player.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static event Action <GameObject> returnCheck;
    bool siepieLocked = false, takkieLocked = false; 
    public RectTransform imageTarget;
    public GameObject image;
    public Image Title;
    public float LerpSpeed;

    private void Start()
    {
        ControllerMover.controlCheck += CheckControllers;
    }
    private void FixedUpdate()
    {
        if (Vector3.Lerp(image.transform.position, imageTarget.position, LerpSpeed*Time.deltaTime) != imageTarget.position)
        {
            Debug.Log("Lerping");
            image.transform.position = Vector3.Lerp(image.transform.position, imageTarget.position, LerpSpeed*Time.deltaTime);
        }
    }

    public void CheckControllers(GameObject controller, int location)
    {
        if(location == 0 && takkieLocked == false)
        {
            takkieLocked = true;
            returnCheck?.Invoke(controller);
        }
        else if(location == 2 && siepieLocked == false)
        {
            siepieLocked = true;
            returnCheck?.Invoke(controller);
        }
        else
        {
            Debug.LogWarning("Controller set failed.");
        }

    }
}
