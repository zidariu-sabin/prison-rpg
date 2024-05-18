using System;
using  System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class NewBehaviourScript : MonoBehaviour
{
    Rigidbody rigidbody;
    private float _speed=5;
    private Vector2 _move;
    private float _dash;
    private Vector2 _clickPos;
    private Rangeweapon _Pistol;
    private InputManager _inputManager;
    private bool _Tap;
    
    private void Awake()
    {
        _Pistol = FindObjectOfType<Rangeweapon>();
        _inputManager = new InputManager(); 
    }

    public void OnEnable()
    {
     
        
        _inputManager.PlayerGameplay.Move.performed += OnMove;
        _inputManager.PlayerGameplay.Move.canceled += OnMove;
        _inputManager.PlayerGameplay.Move.Enable();
        
       _inputManager.PlayerGameplay.Dash.performed += OnDash;
       _inputManager.PlayerGameplay.Dash.Enable();
       
       _inputManager.PlayerGameplay.Strike.performed += OnStrikePerformed;
       _inputManager.PlayerGameplay.Strike.canceled += OnStrikeCanceled;
       _inputManager.PlayerGameplay.Strike.Enable();

       _inputManager.PlayerGameplay.Reload.performed += OnReloadPerformed;
       _inputManager.PlayerGameplay.Reload.Enable();
    }

    //Reads the inputted _move
    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        _dash = context.ReadValue<float>();
            Debug.Log("spacebar pressed:" + _dash);
    }

   

  
    private void OnStrikePerformed(InputAction.CallbackContext context)
    {
        SetAllInactive ();
        if (context.interaction is HoldInteraction) {
            _Pistol.shooting = true;
            Debug.Log("Hold interaction - performed");
        } else
        {
            _Tap = true;
            _Pistol.Shoot(_Tap);
            Debug.Log("Tap Interaction - performed");
        }
    }

    private void OnStrikeCanceled(InputAction.CallbackContext context)
    {
        SetAllInactive ();
        _Pistol.shooting = false;
        Debug.Log("Hold interaction - canceled");
    }
    
    

    public void OnReloadPerformed(InputAction.CallbackContext context)
    {   
            _Pistol.reloading = true;
        
    }

    private void OnDisable()
    {
        //moveInputAction.Disable();
        _inputManager.PlayerGameplay.Move.performed -= OnMove;
        _inputManager.PlayerGameplay.Move.canceled -= OnMove;
        _inputManager.PlayerGameplay.Move.Disable();
        
        _inputManager.PlayerGameplay.Dash.performed -= OnDash;
        _inputManager.PlayerGameplay.Dash.Disable();
        
        _inputManager.PlayerGameplay.Strike.performed -= OnStrikePerformed;
        _inputManager.PlayerGameplay.Strike.canceled -= OnStrikeCanceled;
        _inputManager.PlayerGameplay.Strike.Disable();
        
        _inputManager.PlayerGameplay.Reload.performed -= OnReloadPerformed;
        _inputManager.PlayerGameplay.Reload.Disable();
        
    }
    private void SetAllInactive ()
    {
        _Tap = false;
        _Pistol.shooting = false;
    }

    private Camera _viewCamera;
    void Start()
    {
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
