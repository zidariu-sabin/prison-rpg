using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
//Library for input system
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody rigidbody;
    private float speed=5;
    private Vector2 move;
    private Vector2 clickPos;
    private Transform m_transform;

    //Reads the inputted move
    public void OnMove(InputAction.CallbackContext context)
    {   //move is a vector 2 for wasd inputs
        move = context.ReadValue<Vector2>();
      //  Debug.Log(context.ReadValue<Vector2>());
    }

    public void OnStrike(InputAction.CallbackContext context)
    {   if(context.performed)
        {
            clickPos = context.ReadValue<Vector2>();
            Debug.Log(clickPos);
        }
    }

    private Camera viewCamera;
    void Start()
    {
        m_transform = this.transform;
        //  rigidbody = GetComponent<Rigidbody>();
          viewCamera = Camera.main;
    }
    
    void Update()
    {  
        movePlayer();
        pointerRotation();
    }

    public void movePlayer()
    {   
        //so character doesn't snap back to original place
        if (move.sqrMagnitude > 0.1f)
        {   //movement will assign move values to a vector
            Vector3 movement = new Vector3(move.x, 0f, move.y);
             
            //rotate to look towards movement point with speed 0.15
          //  transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
            transform.Translate(Time.deltaTime * movement * speed, Space.World);
        }
    }

    public void pointerRotation()
    {
        Vector3 mousePos = viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,viewCamera.transform.position.y));
        transform.LookAt(mousePos + Vector3.up*transform.position.y);
    }
}
