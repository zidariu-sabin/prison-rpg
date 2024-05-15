using  System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
//Library for input system
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody rigidbody;
    private float _speed=5;
    private Vector2 _move;
    private float _dash;
    private Vector2 _clickPos;
    private Transform _mtransform;
    private Rangeweapon _Pistol;

    //Reads the inputted _move
    public void OnMove(InputAction.CallbackContext context)
    {   //_move is a vector 2 for wasd inputs
        _move = context.ReadValue<Vector2>();
        Debug.Log(_move.x + "" + _move.y + "");
    }

    public void OnDash(InputAction.CallbackContext context)
    {   if(context.started)
        {
            _dash = context.ReadValue<float>();
            Debug.Log("spacebar pressed:" + _dash);
        }
    }

    public void OnStrike(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (_Pistol.automatic)
            {
                if (context.interaction is HoldInteraction || context.interaction is TapInteraction)
                {    Debug.Log("Hold Interaction");
                    _Pistol.shooting = true;
                }
            }
            else
            { 
                if (context.interaction is TapInteraction)
                {   Debug.Log("Tap Interaction");
                    _Pistol.shooting = true;
                }
            }
        }

        if (context.canceled )
        {   Debug.Log("Context canceled");
            _Pistol.shooting = false;
        }
    }

    public void OnReload(InputAction.CallbackContext context)
    {   if(context.performed)
        {
            _Pistol.reloading = true;
        }
    }

    private Camera _viewCamera;
    void Start()
    {
        _mtransform = this.transform;
        //  rigidbody = GetComponent<Rigidbody>();
          _Pistol = FindObjectOfType<Rangeweapon>();
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
            
            transform.Translate(CalculateMovementDistance(movement), Space.World);
            if (_dash == 1)
            {   
                Vector3 startPosition = transform.position;
                Vector3 desiredPosition = startPosition + CalculateMovementDistance(movement*40);
                transform.position = Vector3.Lerp(startPosition, desiredPosition, 1f);
                _dash = 0;
            }
    }
    

    public void PointerRotation()
    {
        Vector3 mousePos = _viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,_viewCamera.transform.position.y));
        transform.LookAt(mousePos + Vector3.up*transform.position.y);
    }
    
    public Vector3 CalculateMovementDistance(Vector3 movement)
    {
        return Time.deltaTime * movement * _speed;
    }
}
