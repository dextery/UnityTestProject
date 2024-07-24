using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncherScript : WeaponScript
{
    [SerializeField] GameObject grenadePrefab;
    protected override void SetupTracer()
    {
        
    }
    public override void Shoot()
    {
        if (!canShoot) 
        {
            return;
        }
        canShoot = false;
        coolDown = gunCooldown;
        Vector3 shotLocation = player.GetMousePosition();
        GameObject grenade = Instantiate(grenadePrefab, shotPos.position, Quaternion.identity);
        grenade.GetComponent<GrenadeShell>().Setup(shotLocation);
    }
}
