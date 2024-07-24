using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeShell : MonoBehaviour
{
    [SerializeField] GameObject GrenadeExplodeVFXPrefab;
    [SerializeField] float speed;
    [SerializeField] float radius;

    [SerializeField] float damage;
    [SerializeField] LayerMask enemyMask;
    private Vector3 destination;

    public void Setup(Vector3 shotLocation)
    {
        destination = shotLocation;
    }

    private void Update()
    {
        Vector3 moveDir = (destination - transform.position).normalized;
        transform.position += moveDir * speed * Time.deltaTime;
        float reachedDetonationDistance = .2f;
        if(Vector3.Distance(transform.position, destination) < reachedDetonationDistance)
        {
            Collider[] enemyArray = Physics.OverlapSphere(destination, radius, enemyMask);
            foreach(Collider enemy in enemyArray) 
            {
                Debug.Log("Damaged for "+ damage);
                enemy.gameObject.GetComponent<Actor>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
    private void OnDestroy() 
    {
        Instantiate(GrenadeExplodeVFXPrefab, transform.position, Quaternion.identity);    
    }
}
