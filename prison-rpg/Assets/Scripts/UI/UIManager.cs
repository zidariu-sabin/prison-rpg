using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{  
   [SerializeField]
   private Text _ammoText;
   private Rangeweapon _rangeWeapon;

   public void Start()
   {
      _rangeWeapon = FindObjectOfType<Rangeweapon>();
   }

   public void Update()
   {
      UpdateAmmo(_rangeWeapon.bulletsInMagazine,_rangeWeapon.magazines);
      Reloading();
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
}
