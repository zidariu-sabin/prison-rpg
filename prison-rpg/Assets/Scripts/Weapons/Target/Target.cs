using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Target : MonoBehaviour
{
    public float maxHealth=100;
    public float currentHealth;
    public bool valid = true;

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (currentHealth <= 0 && valid)
        {
            if (currentHealth <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
