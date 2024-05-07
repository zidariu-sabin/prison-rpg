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
    public bool reloading;
    
    // Start is called before the first frame update
    void Start()
    {
        bulletsInMagazine = magazineSize;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (bulletsInMagazine <= 0)
        {
            ReloadMagazine();
        }
        
    }

    private void ReloadMagazine()
    {
        bulletsInMagazine = magazineSize;
    }

    public void Shoot()
    {
        bulletsInMagazine--;
        Debug.Log("Bullets:"+ bulletsInMagazine);
    }
}
