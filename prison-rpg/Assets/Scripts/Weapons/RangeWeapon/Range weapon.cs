using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = System.Numerics.Quaternion;

public class Rangeweapon : MonoBehaviour
{
   // private Player Player;
    //Need to abstract target mothod to differenciate weapon of enemy vs weapon of player so enemy can shoot according to weapon and poklayer according to input
    //in the present case: select the mouse position to be the target towards which the gun is going to shoot
   // public Vector3 shootingTarget;
    public float weaponDamage=10f, bulletSpeed=30f;
    public int magazineSize = 30, magazines = 4, bulletsInMagazine;
    public float reloadTime=3,range=10000.0f,bulletsPerShot=1;
    public bool automatic;
    public bool shooting=false,reload=false,reloading=false;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    public Rigidbody rb;
    public BoxCollider coll;
    
    
    void Awake()
    {
        bulletsInMagazine = magazineSize;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<BoxCollider>();
    }
        
    void FixedUpdate()
    {
        StartCoroutine(ReloadMagazine());
            Shoot();
    }

    public IEnumerator ReloadMagazine()
    {   
        if (reload && magazines>0)
        {
            reload = false;
            reloading = true;
            yield return new WaitForSeconds(reloadTime);
            //reloading has to be set to false after function call so function doesn't get triggered multiple times in the reloadtime
            bulletsInMagazine = magazineSize;
            magazines--;
            reloading = false;
            
        }
    }
    

    public void Shoot(bool tap=false)
    {
        if (tap)
        {
            shooting = tap;
        }
        
        if (shooting && bulletsInMagazine>0 && !reloading)
        {
            bulletsInMagazine--; 
            if(bulletsPerShot>1)
            {
                float bulletSpread = 5;
                for (int i = 0; i < bulletsPerShot; i++)
                {
                    //function needs to rotate bullet spawnpoint direction in  case spread>0
                    CreateBullets(bulletSpread);
                    bulletSpread -= 15/(bulletsPerShot-1);
                }
            }
            else
            {
                CreateBullets(0);
            }
        }
         
            
        if (tap)
        {
            shooting = false;   
        }
    }

    public void CreateBullets(float bulletSpread)
    {   
        Vector3 spread = new Vector3(0, bulletSpread, 0);
        UnityEngine.Quaternion rotation = UnityEngine.Quaternion.Euler(spread) * bulletSpawnPoint.rotation;
        GameObject currentBullet = Instantiate(bullet, bulletSpawnPoint.position, rotation);
        GiveBulletStats(currentBullet.GetComponent<BulletScript>());
    }

    public void GiveBulletStats(BulletScript bulletInstance)
    { 
        bulletInstance.carriedDamage = weaponDamage; 
        bulletInstance.carriedSpeed = bulletSpeed;   
        bulletInstance.carriedSpeed = bulletSpeed;
        bulletInstance.carriedDistance = range;
    }
}
