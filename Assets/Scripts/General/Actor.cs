using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float movementSpeed = 4;

    public virtual void TakeDamage(float damage)
    {
        health-=damage;
        if (health<=0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetSpeed() 
    {
        return movementSpeed;
    }
}
