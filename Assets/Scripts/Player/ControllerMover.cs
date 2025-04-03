using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class ControllerMover : MonoBehaviour
{
    TextMeshProUGUI screenText;
    InputDevice device;
    Transform[] Targets = new Transform[3];
    Transform BG;
    public static Action<GameObject, int, InputDevice> controlCheck;
    bool locked = false;
    float moveValue;
    int location = 1;

    private void Start()
    {
        BG = GameObject.Find("Sky").transform;
        transform.SetParent(BG);
        device = GetComponent<PlayerInput>().devices[0];
        screenText = GetComponent<TextMeshProUGUI>();
        gameObject.name = device.name;
        screenText.text = device.name;
        TransMenuManager.returnCheck += Checked;
        Targets[0] = GameObject.Find("Left").transform;
        Targets[1] = GameObject.Find("Middle").transform;
        Targets[2] = GameObject.Find("Right").transform;
        transform.position = Targets[1].position;
    }

    public void OnMove(InputValue value)
    {
      moveValue = value.Get<Vector2>().x;
    }

    public void OnInteract()
    {
        if (!locked)
        {
            controlCheck?.Invoke(this.gameObject, location, device);
        }
        else
        {
            screenText.color = Color.white;
            locked = false;
        }
    }

    public void Checked(GameObject controllerChecked)
    {
      if(this.gameObject == controllerChecked)
      {
            screenText.color = Color.white;
            locked = true;
      }  
    }

    private void FixedUpdate()
    {
        if (!locked)
        {
            if (moveValue < 0f && location != 0) location--;
            
            else if (moveValue > 0f && location != 2)location++;
        }
        else
        {
            screenText.color = Color.green;
        }
        transform.position = Vector3.Lerp(transform.position, Targets[location].position, 0.5f);
    }
}
