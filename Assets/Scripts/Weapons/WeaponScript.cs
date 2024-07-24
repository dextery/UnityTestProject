using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEditor.ShaderGraph.Internal;
using UnityEditor.Timeline;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    
    [SerializeField] protected Transform shotPos;
    [SerializeField] protected float shotRange;
    [SerializeField] private ShootType weaponType;
    [SerializeField] protected float gunCooldown;
    [SerializeField] protected float gunDamage;
    [SerializeField] protected LayerMask enemyMask;

    private LineRenderer bulletTracer;
    protected PlayerScript player;
    public int gunId;
    public float gunTimer;

    protected float shotDistance;
    protected float coolDown;
    protected bool canShoot;
    private void Start()
    {
        SetupTracer();
        if (weaponType==ShootType.ShortWait) 
        {
            gunCooldown /=2;
        }
        canShoot=true;

    }
    private void Update()
    {
        if (coolDown>0)
        {
            coolDown-=Time.deltaTime;
        } 
        else
        {
            canShoot=true;
        }
    }
    protected virtual void SetupTracer()
    {
        bulletTracer = GetComponentInChildren<LineRenderer>();
        bulletTracer.SetPosition(1, new Vector3(0, 0, shotRange));
        bulletTracer.enabled = false;
    }
    public virtual void Shoot()
    {
        if (!canShoot) 
        {
            return;
        }
        canShoot = false;
        coolDown = gunCooldown;
        shotDistance = shotRange;
        Ray ray = new Ray(shotPos.position, shotPos.forward);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit, shotDistance, enemyMask))
        {
            shotDistance = hit.distance;
            if(hit.collider.GetComponent<Actor>())
            {
                hit.collider.GetComponent<Actor>().TakeDamage(gunDamage);
            }
        }
        StartCoroutine("RenderShot");
    }

    IEnumerator RenderShot ()
    {
        bulletTracer.enabled = true;
        bulletTracer.SetPosition(1, new Vector3 (0, 0, shotDistance));
        yield return new WaitForSeconds(0.02f);
        bulletTracer.enabled = false;
    }
    public void SetCurrentPlayer(PlayerScript var) 
    {
        player = var;
    }

    public enum ShootType
    {
        LongWait,
        ShortWait
    }
}
