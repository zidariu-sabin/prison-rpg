using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    
    public bool slotFull,active;
    public GameObject weapon;
    public Rangeweapon rangeWeapon;
    void Start()
    {
        
    }

    void OnEnable()
    {
        if (GetComponentInChildren<Rangeweapon>() != null)
        {   
            rangeWeapon = GetComponentInChildren<Rangeweapon>();
            weapon = rangeWeapon.gameObject;
        }
    }
    
    void Update()
    {
        
    }
}
