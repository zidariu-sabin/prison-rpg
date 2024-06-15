﻿using System;
using System.Collections;
using System.Collections.Generic;
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
    //public Transform player, _weaponSlot;
    private Transform _weaponSlot;
    public GameObject player;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;
   


    private void OnEnable()
    {
        //Setup
        _weaponSlot = player.GetComponentInChildren<WeaponContainer>().gameObject.GetComponentInChildren<WeaponSlot>().gameObject.GetComponent<Transform>(); 
        _rangeWeaponScript = GetComponent<Rangeweapon>();
        _rangeWeaponRb = _rangeWeaponScript.rb;
        _coll = _rangeWeaponScript.coll;
        if (_weaponSlot == null)
        {
            Debug.Log("Slot not found "+_weaponSlot);
        }
        else
        {
            Debug.Log("Slot found "+_weaponSlot);

        }
      
    }

    private void Start()
    {        

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
            slotFull = true;
        }
    }
    

    private void Update()
    {
        //Check if player is in range and "E" is pressed
        transform.rotation = _weaponSlot.rotation;
        Vector3 distanceToPlayer = player.transform.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && player.GetComponent<Player>().equip!=0 && !slotFull)
        {
            PickUp();
        }

        //Drop if equipped and "Q" is pressed
        if (equipped && player.GetComponent<Player>().drop!=0)
        {
            Drop();
        }
    }

    private void PickUp()
    {
        equipped = true;
        slotFull = true;

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(_weaponSlot);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        _rangeWeaponRb.isKinematic = true;
        _coll.isTrigger = true;

        //Enable script
        _rangeWeaponScript.enabled = true;
    }

    private void Drop()
    {
        equipped = false;
        slotFull = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        _rangeWeaponRb.isKinematic = false;
        _coll.isTrigger = false;

        //Gun carries momentum of player
        _rangeWeaponRb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        _rangeWeaponRb.AddForce(_weaponSlot.forward * dropForwardForce, ForceMode.Impulse);
        _rangeWeaponRb.AddForce(_weaponSlot.up * dropUpwardForce, ForceMode.Impulse);
        //Add random rotation
        float random = Random.Range(-1f, 1f);
        _rangeWeaponRb.AddTorque(new Vector3(random, random, random) * 10);

        //Disable script
        _rangeWeaponScript.enabled = false;
    }
}