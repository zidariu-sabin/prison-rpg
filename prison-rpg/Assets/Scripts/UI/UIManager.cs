using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{  
   [SerializeField]
   private Text _ammoText;
   public void UpdateAmmo(int bullets, int magazine)
   {
      _ammoText.text = "Ammo: " + bullets +"/"+magazine;
   }
}
