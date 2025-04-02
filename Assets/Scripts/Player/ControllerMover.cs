using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerMover : MonoBehaviour
{
    public Transform[] Targets;
    public static Action<GameObject, int> controlCheck;
    bool locked = false;
    float moveValue;
    int location = 1;

    private void Start()
    {
        MainMenuManager.returnCheck += Checked;
    }

    public void OnMove(InputValue value)
    {
      moveValue = value.Get<Vector2>().x;
    }

    public void OnInteract()
    {
        if (!locked)
        {
            controlCheck?.Invoke(this.gameObject, location);
        }
        else
        {
            locked = false;
        }
    }

    public void Checked(GameObject controllerChecked)
    {
      if(this.gameObject == controllerChecked)
      {
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
        transform.position = Vector3.Lerp(transform.position, Targets[location].position, 0.5f);
    }
}
