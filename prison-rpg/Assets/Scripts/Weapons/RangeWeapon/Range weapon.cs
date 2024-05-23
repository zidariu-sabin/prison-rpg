using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Rangeweapon : MonoBehaviour
{
   // private Player Player;
    //Need to abstract target mothod to differenciate weapon of enemy vs weapon of player
    //in the present case: select the mouse position to be the target towards which the gun is going to shoot
   // public Vector3 shootingTarget;
    public float damage=10f;
    public int magazineSize = 30, magazines = 4, bulletsInMagazine;
    public float reloadTime=3,range=10000.0f,bulletsPerShot, bulletSpeed=10f,bulletSpread;
    public bool automatic;
    public bool shooting=false,reloading=false,readyToShoot=true;
    private Camera _viewCamera;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    private Target target;
    
    
    void Start()
    {
        bulletsInMagazine = magazineSize;
        _viewCamera = Camera.main;
    }
        
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
        
        if (shooting && bulletsInMagazine>0)
        {
            bulletsInMagazine--;
            
            for (int i = 0; i < bulletsPerShot; i++)
            {
                bulletSpread *= -1;
                CreateBullets(bulletSpread);
            }
        }

       
            
        if (tap)
        {
            shooting = false;
        }
    }

    public void CreateBullets(float x)
    {
        RaycastHit hit;
        Ray ray = new Ray(bulletSpawnPoint.position,  bulletSpawnPoint.forward);
        Vector3 targetPoint;    
        if (Physics.Raycast(ray, out hit, range))
        {
            targetPoint = hit.point;
            target = hit.transform.GetComponent<Target>();
            target?.TakeDamage(damage);
            // GameObject currentBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            // currentBullet.GetComponent<Rigidbody>().velocity = bulletSpawnPoint.forward * bulletSpeed;
        }
        else targetPoint = ray.GetPoint(20);
        Vector3 directionWithoutSpread = targetPoint - bulletSpawnPoint.position;
        
        // spread for shotgun to bullets can go to the side in cone
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, 0, 0);
            
        GameObject currentBullet = Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
        // Rotate bullet to shoot direction
        currentBullet.transform.forward = directionWithSpread.normalized;
            
        //Add forces to bullet
        currentBullet.GetComponent<Rigidbody>().AddForce(directionWithSpread.normalized * bulletSpeed, ForceMode.Impulse);

    }
}
