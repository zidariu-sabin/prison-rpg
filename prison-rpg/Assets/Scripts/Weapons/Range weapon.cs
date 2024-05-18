using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rangeweapon : MonoBehaviour
{
    public float damage=10f;
    public int magazineSize=30;
    public int bulletsInMagazine;
    public int magazines=4;
    public float reloadTime=3,range=100f;
    public bool automatic;
    public bool shooting=false,reloading=false,readyToShoot;
    
    
    // Start is called before the first frame update
    void Start()
    {
        bulletsInMagazine = magazineSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ReloadMagazine();
        Shoot();
        
    }

    public void ReloadMagazine()
    {
        if (reloading)
        {
            bulletsInMagazine = magazineSize;
            magazines--;   
        }

        reloading = false;
    }

    public void Shoot(bool tap=false)
    {
        if (tap)
        {
            shooting = tap;
        }
        
        if (shooting && bulletsInMagazine >0)
        {
            bulletsInMagazine--; 
        }
        
        if (tap)
        {
            shooting = false;
        }
    }
    
}
