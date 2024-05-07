using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{  
   [SerializeField]
   private Text _ammoText;
   private Rangeweapon _Pistol;

   public void Start()
   {
      _Pistol = FindObjectOfType<Rangeweapon>();
   }

   public void Update()
   {
      UpdateAmmo(_Pistol.bulletsInMagazine,_Pistol.magazines);
   }
   public void UpdateAmmo(int bullets, int magazine)
   {
      _ammoText.text = "Ammo: " + bullets +"/"+magazine;
   }
}
