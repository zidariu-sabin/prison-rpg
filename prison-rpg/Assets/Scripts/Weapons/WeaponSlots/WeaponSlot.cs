using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    
    public bool full,active;
    public GameObject weapon;
    public Rangeweapon rangeWeapon;
    void Start()
    {
        
    }

    void Awake()
    {
        rangeWeapon = GetComponentInChildren<Rangeweapon>(true);
        weapon = rangeWeapon.gameObject;
    }
    
    void Update()
    {
        
    }
}