using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoUIManager : MonoBehaviour
{  
   [SerializeField]
   private Text _ammoText;
   private Rangeweapon _rangeWeapon;
   public WeaponContainer _weaponContainer;

   public void Start()
   {  
      _weaponContainer = FindObjectOfType<WeaponContainer>();
     // _rangeWeapon = WeaponContainer.we();
   }

   public void Update()
   {  
      GetCurrentWeapon();
      if(_rangeWeapon)
      {  
         UpdateAmmo(_rangeWeapon.bulletsInMagazine, _rangeWeapon.magazines);
         Reloading();
      }
      else
      {
         _ammoText.text = "Ammo: "+"\u221e";
      }
   }
   public void UpdateAmmo(int bullets, int magazine)
   {
      _ammoText.text = "Ammo: " + bullets +"/"+magazine;
   }

   public void Reloading()
   {
      if(_rangeWeapon.reloading)
      {
         _ammoText.text += System.Environment.NewLine + " Reloading...";
      }
   }

   public void GetCurrentWeapon()
   {
      if (_weaponContainer._weaponSlot1.weapon.GetComponent<Rangeweapon>().isActiveAndEnabled)
      {
         _rangeWeapon = _weaponContainer._weaponSlot1.weapon.GetComponent<Rangeweapon>();
      }
      else if (_weaponContainer._weaponSlot2.weapon.GetComponent<Rangeweapon>().isActiveAndEnabled)
      {
         _rangeWeapon = _weaponContainer._weaponSlot2.weapon.GetComponent<Rangeweapon>();
      }
      else
      {
         _rangeWeapon = null; // or some default value
      }  
   }
}
