using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class TakkieMovement : MonoBehaviour
{
  bool controlLocked, running = false;
  int currentWalkAnim;
  float moveSpeed;
  public float WalkSpeed, RunSpeed;
  Rigidbody2D rb;
  Vector2 moveValue;
  Animator animator;
  Vector2 zero = new Vector2(0, 0);

  //Neccesary components fetched on enable -Henry
  private void OnEnable()
  {
    animator = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
  }

  //Function is called when Takkie receives the OnTakkieMove input from the PlayerInput component
  //Value of the Vector2 value is read as Value -Henry
  public void OnMove(InputValue value)
  {
    moveValue = value.Get<Vector2>();
  }

  public void Onrun()
    {
        running = true;
    }

  private void FixedUpdate()
  {
        if (!controlLocked)
        {
            //Velocity is changed based on the value of the vector received to moveValue during OnMove. -Henry
            //while(emoting == false)
            Vector2 move = transform.up * moveValue.y + transform.right * moveValue.x;

            //Animator receives position of the joystick relative to a unit circle. -Henry
            animator.SetFloat("X", moveValue.x);
            animator.SetFloat("Y", moveValue.y);

            //Animator bool is toggle on and off depending on if the move value is equal to zero. -Henry
            if (move == zero)
            {
                animator.SetBool("Moving", false);
            }
            else
            {
                animator.SetBool("Moving", true);
            }

            //Checks if the player is running and switches speed accordingly depending on running bool -Henry
            if (!running)
            {
                moveSpeed = WalkSpeed;
            }
            else
            {
                moveSpeed = RunSpeed;
            }
            running = false;

            //Velocity is changed
            rb.velocity = move * moveSpeed;


            /*Finds out in what direction the player is moving and sends a value to the animator to notify it what idle animation it should switch to
            When player is not moving based on that value. In other words, if the player is moving up and then comes to a stop, the animator will start
            playing the upwards idle animation. -Henry */
            if (moveValue.y > 0.2 && -0.5f < moveValue.x && moveValue.x < 0.5f)
            {
                animator.SetInteger("WalkAnim", 1);
            }

            else if (moveValue.y < -0.2 && -0.5f < moveValue.x && moveValue.x < 0.5f)
            {
                animator.SetInteger("WalkAnim", 3);
            }

            else if (moveValue.x < -0.2 && -0.5f < moveValue.y && moveValue.y < 0.5f)
            {
                animator.SetInteger("WalkAnim", 2);
            }

            else if (moveValue.x > 0.2 && -0.5f < moveValue.y && moveValue.y < 0.5f)
            {
                animator.SetInteger("WalkAnim", 4);
            }
        }
        else
        {
            rb.velocity = new Vector2(0, 0);
        }
    }

    public void LockMove()
    {
        if (controlLocked) controlLocked = false;
        else controlLocked = true;
    }

}
