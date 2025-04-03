using Player.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class TransMenuManager : MonoBehaviour
{
    public static event Action <GameObject> returnCheck;
    bool siepieLocked = false, takkieLocked = false;
    InputDevice siepieDevice, takkieDevice;
    public RectTransform imageTarget;
    public GameObject image,siepiePrefab, takkiePrefab;
    public float LerpSpeed;

    private void Start()
    {
       DontDestroyOnLoad(gameObject);
       ControllerMover.controlCheck += CheckControllers;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void FixedUpdate()
    {
        if(SceneManager.GetActiveScene().name == "Main Menu")
        {
            if (Vector3.Lerp(image.transform.position, imageTarget.position, LerpSpeed * Time.deltaTime) != imageTarget.position)
            {
                image.transform.position = Vector3.Lerp(image.transform.position, imageTarget.position, LerpSpeed * Time.deltaTime);
            }
        }
    }

    public void CheckControllers(GameObject controller, int location, InputDevice ID)
    {
        if(location == 0 && takkieLocked == false)
        {
            takkieLocked = true;
            takkieDevice = ID;
            returnCheck?.Invoke(controller);
        }
        else if(location == 2 && siepieLocked == false)
        {
            siepieLocked = true;
            siepieDevice = ID;
            returnCheck?.Invoke(controller);
        }
        else
        {
            Debug.LogWarning("Controller set failed.");
        }
        if(siepieLocked && takkieLocked)
        {
            Debug.Log("loading next scene.");
            SceneManager.LoadScene("Stockholm");
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode lsm) 
    {
     if (scene.name != "Main Menu")
     {
            PlayerInput.Instantiate(siepiePrefab, -1, null, -1, siepieDevice);
            GameObject.FindGameObjectWithTag("Cat").transform.position = GameObject.FindGameObjectWithTag("CatSpawn").transform.position;
            PlayerInput.Instantiate(takkiePrefab, -1, null, -1, takkieDevice);
            GameObject.FindGameObjectWithTag("Cat").transform.position = GameObject.FindGameObjectWithTag("CatSpawn").transform.position;
        }
    }
}
