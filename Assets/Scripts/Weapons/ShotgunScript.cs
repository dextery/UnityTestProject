using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShotgunScript : WeaponScript
{
    [SerializeField] Transform[] pelletLocations;
    private List<LineRenderer> pelletTracers = new List<LineRenderer>();
    protected override void SetupTracer()
    {
        if (pelletTracers.Count != 0) //List has already been filled
        {
            return;
        }
        foreach(Transform pelletLoc in pelletLocations)
        {
            LineRenderer pelletTracer = pelletLoc.gameObject.GetComponentInChildren<LineRenderer>();
            pelletTracer.SetPosition(1, new Vector3(0, 0, shotRange));
            pelletTracer.enabled = false;
            pelletTracers.Add(pelletTracer);
        }
    }
    public override void Shoot()
    {
        //base.Shoot();
        if (!canShoot) 
        {
            return;
        }
        float dmg =0;
        canShoot = false;
        coolDown = gunCooldown;
        shotDistance = shotRange;
        foreach(Transform pelletLoc in pelletLocations)
        {
            Ray ray = new Ray(pelletLoc.position, pelletLoc.forward);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, shotDistance, enemyMask))
            {
                shotDistance = hit.distance;
                if(hit.collider.GetComponent<Actor>())
                {
                    hit.collider.GetComponent<Actor>().TakeDamage(gunDamage);
                    dmg+=gunDamage;
                }
            }
        }
        Debug.Log(dmg);
        StartCoroutine("RenderShotgun");
    }
    IEnumerator RenderShotgun ()
    {
        foreach(var pellet in pelletTracers) 
        {
            pellet.enabled = true;
            pellet.SetPosition(1, new Vector3 (0, 0, shotDistance));
        }
        yield return new WaitForSeconds(0.02f);
        foreach(var pellet in pelletTracers)
        {
            pellet.enabled = false;
        }
    }
}
