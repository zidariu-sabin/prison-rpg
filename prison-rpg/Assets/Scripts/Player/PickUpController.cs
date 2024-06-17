﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PickUpController : MonoBehaviour
{
    private GameObject _weaponContainer;
    // i want to be able to pick up a weapon
    private Rangeweapon  _rangeWeaponScript;
    //public Rigidbody _rangeWeaponRb;
    //we want to use the rigid body of the weapon we see nearby
    private BoxCollider _coll;
    private Rigidbody _rangeWeaponRb;
    private GameObject _weaponSlot; 
    private WeaponSlot _weaponSlotScript;
    public GameObject player;

    public float pickUpRange=15;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
   


    private void OnEnable()
    {
        //Setup
        InitializeWeaponSlot();
        _rangeWeaponScript = GetComponent<Rangeweapon>();
        _rangeWeaponRb = _rangeWeaponScript.rb;
        _coll = _rangeWeaponScript.coll;
      
    }

    

    private void Start()
    {        
        InitializeWeaponSlot();
        if (!equipped)
        {
            _rangeWeaponScript.enabled = false;
            _rangeWeaponRb.isKinematic = false;
            _rangeWeaponRb = _rangeWeaponScript.rb;
            _coll.isTrigger = false;
        }
        if (equipped)
        {
            _rangeWeaponScript.enabled = true;
            _rangeWeaponRb.isKinematic = true;
            _coll.isTrigger = true;
            _weaponSlotScript.slotFull = true;
        }
    }

    private void InitializeWeaponSlot()
    {
        _weaponSlotScript = player.GetComponentInChildren<WeaponContainer>().gameObject.GetComponentInChildren<WeaponSlot>();
    }
    

    private void Update()
    {
        //Check if player is in range and "E" is pressed
       if(equipped)
       {
           transform.rotation = _weaponSlotScript.gameObject.transform.rotation;
       }
        
        if (!equipped  && player.GetComponent<Player>().equip!=0 /* && !slotFull */ && !_weaponSlotScript.slotFull)
        {
            Vector3 distanceToPlayer = player.transform.position - transform.position;
            Debug.Log(distanceToPlayer.magnitude);
            if(distanceToPlayer.magnitude <= pickUpRange)
            {
                PickUp();
            }
        }

        //Drop if equipped and "Q" is pressed
        if (equipped && player.GetComponent<Player>().drop!=0)
        {
            Drop();
        }
    }
    
    
    private void PickUp()
    {
        InitializeWeaponSlot();
        equipped = true;
        _weaponSlotScript.slotFull = true;
        
        //Make weapon a child of the camera and move it to default position
        transform.SetParent(_weaponSlotScript.gameObject.transform);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        _rangeWeaponRb.isKinematic = true;
        _coll.isTrigger = true;

        //Enable script
        _rangeWeaponScript.enabled = true;
        
       // _weaponSlot.GetComponent<WeaponSlot>().slotFull = true;
    }

    private void Drop()
    {
        equipped = false;
        _weaponSlotScript.slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        _rangeWeaponRb.isKinematic = false;
        _coll.isTrigger = false;

        //Gun carries momentum of player
        _rangeWeaponRb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        _rangeWeaponRb.AddForce(_weaponSlotScript.gameObject.transform.forward * dropForwardForce, ForceMode.Impulse);
        _rangeWeaponRb.AddForce(_weaponSlotScript.gameObject.transform.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        _rangeWeaponRb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
        _rangeWeaponScript.enabled = false;
        //setting weaponslot.rangeweapon to null so that  it drops the refference on the weapon when dropped(sloppy but gets the job done)
         _weaponSlotScript.rangeWeapon = null;
       //  _weaponSlotScript.weapon = null;
    }
}