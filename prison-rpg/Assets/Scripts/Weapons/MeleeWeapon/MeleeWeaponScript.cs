using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MeleeWeapon : MonoBehaviour
{
    
    public bool stun,poison,damage;
    public Animator anim;
    public Player player;
    public bool hit = false;
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("attacking",true);
        }
        else if(Input.GetButtonDown("Fire1"))
        {
            anim.SetBool("attacking",false);
        }
        
    }
}
