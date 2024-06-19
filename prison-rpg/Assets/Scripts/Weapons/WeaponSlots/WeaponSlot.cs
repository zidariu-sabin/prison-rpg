using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    
    public bool slotFull,active;
    public GameObject weapon;
    public Rangeweapon rangeWeapon;
  

    void OnEnable()
    {
       if (GetComponentInChildren<Rangeweapon>() != null)
       {
           slotFull = true;
           rangeWeapon = GetComponentInChildren<Rangeweapon>();
           weapon = rangeWeapon.gameObject;
       }
       else
       {
           slotFull = false;
       }
    }
    
    void Update()
    {
        
    }
}
