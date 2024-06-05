using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Rigidbody rb;
    private Target target;
    public float carriedSpeed,carriedDamage,carriedDistance;
    private Vector3 spawn;

    void Start()
    {
        rb.AddForce(transform.forward*carriedSpeed);
        spawn = transform.position;
    }

    void Update()
    {
        //i have to destry bullet after travelling past range
        if (Vector3.Distance(spawn, transform.position) > carriedDistance)
        {
            Destroy(gameObject);
        }
    }
 
    void OnCollisionEnter(Collision collision)
    {
        target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            target.TakeDamage(carriedDamage);
          
        }
        Destroy(gameObject);
    }
}
