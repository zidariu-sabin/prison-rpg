using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
//Library for input system
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody rigidbody;
    private float _speed=5;
    private Vector2 _move;
    private Vector2 _clickPos;
    private Transform _mtransform;

    //Reads the inputted _move
    public void OnMove(InputAction.CallbackContext context)
    {   //_move is a vector 2 for wasd inputs
        _move = context.ReadValue<Vector2>();
    }

    public void OnStrike(InputAction.CallbackContext context)
    {   if(context.performed)
        {
            _clickPos = context.ReadValue<Vector2>();
            Debug.Log(_clickPos);
        }
    }

    private Camera _viewCamera;
    void Start()
    {
        _mtransform = this.transform;
        //  rigidbody = GetComponent<Rigidbody>();
          _viewCamera = Camera.main;
    }
    
    void Update()
    {  
        MovePlayer();
        PointerRotation();
    }

    public void MovePlayer()
    {   
            Vector3 movement = new Vector3(_move.x, 0f, _move.y);
            transform.Translate(Time.deltaTime * movement * _speed, Space.World);
    }

    public void PointerRotation()
    {
        Vector3 mousePos = _viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,_viewCamera.transform.position.y));
        transform.LookAt(mousePos + Vector3.up*transform.position.y);
    }
}
