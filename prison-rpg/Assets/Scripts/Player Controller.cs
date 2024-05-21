using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
//Library for input system
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{
    bool moving;
    public Rigidbody rb;
    private Vector2 _move;
    private Vector2 _clickPos;

    private float _speed=5;
    public float maxSpeed = 10f;
    public float decelerationTime = 1f;

    private float _dash;
    private Transform _mtransform;
    float forceMultiplier = 7f;


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
        rb = GetComponent<Rigidbody>();
        _viewCamera = Camera.main;
    }
    
    void Update()
    {
        PointerRotation();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    public void MovePlayer()
    {
        Vector3 movement = new Vector3(_move.x, 0f, _move.y);
        if (_move.x != 0f || _move.y != 0f)
        {
            if (rb.velocity.magnitude < maxSpeed)
            {
                rb.velocity = movement * forceMultiplier;
            }
        }
        else
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
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
