using System;
using  System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    Rigidbody rigidbody;
    private float _speed=5;
    public Vector3 mousePos;
    private Vector2 _move;
    private float _dash;
    private Vector2 _clickPos;
    private Rangeweapon _rangeWeapon;
    private bool _Tap;
    private InputManager _inputManager;
    //must be initialized i awake function
 //   private PickUpController _pickUpController;
    private WeaponContainer _weaponContainer;
    public float drop;
    public float equip;
    
  
    
    private void Awake()
    {
        //_rangeWeapon = FindObjectOfType<Rangeweapon>();
        _weaponContainer = GetComponentInChildren<WeaponContainer>();
        _inputManager = new InputManager();
        SetHoldableItemsInactive();
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
       _inputManager.PlayerGameplay.Reload.canceled += OnReloadCanceled;
       _inputManager.PlayerGameplay.Reload.Enable();
       
       _inputManager.PlayerGameplay.Equip.performed += OnEquip;
       _inputManager.PlayerGameplay.Equip.Enable();
       
       _inputManager.PlayerGameplay.Drop.performed += OnDrop;
       _inputManager.PlayerGameplay.Drop.Enable();
       
       _inputManager.PlayerGameplay.SelectWeaponSlot1.performed += OnSelectWeaponSlot1;
       _inputManager.PlayerGameplay.SelectWeaponSlot1.Enable();
       
       _inputManager.PlayerGameplay.SelectWeaponSlot2.performed += OnSelectWeaponSlot2;
       _inputManager.PlayerGameplay.SelectWeaponSlot2.Enable();
       
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
        SetShootingInactive ();
        if (_rangeWeapon.automatic)
        {
            if (context.interaction is HoldInteraction) {
                _rangeWeapon.shooting = true;
            } else
            {
                _Tap = true;
                _rangeWeapon.Shoot(_Tap);
            }
        }
        else
        if (context.interaction is TapInteraction) {
            _Tap = true;
            _rangeWeapon.Shoot(_Tap);
        }
    }

    private void OnStrikeCanceled(InputAction.CallbackContext context)
    {
        SetShootingInactive ();
        _rangeWeapon.shooting = false;
        Debug.Log("Hold interaction - canceled");
    }
    
    

    public void OnReloadPerformed(InputAction.CallbackContext context)
    {   
            _rangeWeapon.reload = true;
    }
    public void OnReloadCanceled(InputAction.CallbackContext context)
    {   
        _rangeWeapon.reload = false;
    }
    
    public void OnEquip(InputAction.CallbackContext context)
    {   
        equip = context.ReadValue<float>();
        Debug.Log("equip pressed:" + equip);
       // Debug.Log("equip pressed:" + context);
    }
    public void OnDrop(InputAction.CallbackContext context)
    {
       // Debug.Log("drop pressed:" + context.ReadValue<float>());
        drop = context.ReadValue<float>();
        Debug.Log("drop pressed:" + drop);
    }
    public void OnSelectWeaponSlot1(InputAction.CallbackContext context)
    {
        SetHoldableItemsInactive();
        _weaponContainer._weaponSlot1.gameObject.SetActive(true);
        _rangeWeapon =_weaponContainer._weaponSlot1.weapon.GetComponent<Rangeweapon>() ;
     //   _pickUpController = _weaponContainer._weaponSlot1.GetComponent<PickUpController>();
        Debug.Log("weaponSlot selected: 1");
    }
    public void OnSelectWeaponSlot2(InputAction.CallbackContext context)
    {
        SetHoldableItemsInactive();
        _weaponContainer._weaponSlot2.gameObject.SetActive(true);
        _rangeWeapon =_weaponContainer._weaponSlot2.weapon.GetComponent<Rangeweapon>() ;
      //  _pickUpController = _weaponContainer._weaponSlot2.GetComponent<PickUpController>();
        Debug.Log("spacebar slotSelected: 2" );
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
        
        _inputManager.PlayerGameplay.Equip.performed -= OnEquip;
        _inputManager.PlayerGameplay.Equip.Disable();
        
        _inputManager.PlayerGameplay.Drop.performed -= OnDrop;
        _inputManager.PlayerGameplay.Drop.Disable();
        
        _inputManager.PlayerGameplay.SelectWeaponSlot1.performed -= OnSelectWeaponSlot1;
        _inputManager.PlayerGameplay.SelectWeaponSlot1.Enable();
       
        _inputManager.PlayerGameplay.SelectWeaponSlot2.performed -= OnSelectWeaponSlot2;
        _inputManager.PlayerGameplay.SelectWeaponSlot2.Disable();
        
    }
    private void SetShootingInactive ()
    {
        _Tap = false;
        _rangeWeapon.shooting = false;
    }

    private void SetHoldableItemsInactive()
    {
        _weaponContainer._weaponSlot1.gameObject.SetActive(false);
        _weaponContainer._weaponSlot1.weapon.GetComponent<Rangeweapon>().reloading = false;
        _weaponContainer._weaponSlot2.gameObject.SetActive(false);
        _weaponContainer._weaponSlot2.weapon.GetComponent<Rangeweapon>().reloading = false;
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
        mousePos = _viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,_viewCamera.transform.position.y));
        transform.LookAt(mousePos + Vector3.up*transform.position.y);
    }
    
    public Vector3 CalculateMovementDistance(Vector3 movement)
    {
        return Time.deltaTime * movement * _speed;
    }
}
