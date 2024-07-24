using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerSpawner : MonoBehaviour
{
    [SerializeField] private LayerMask dangerLayer;
    [SerializeField] private List<GameObject> dangerZones = new List<GameObject>();

    [SerializeField] Transform spawnChecker;

    [SerializeField] private float xBounds;
    [SerializeField] private float zBounds;
    // Update is called once per frame
    void Update()
    {
        if (dangerZones.Count>0)
        {
            SpawnDangerZoneFromRoster();
        }
    }
    
    private void SpawnDangerZoneFromRoster()
    {
        bool done = false;
        while (!done)
        {
            spawnChecker.transform.position = 
            new Vector3(Random.Range(-xBounds, xBounds), 0, Random.Range(-zBounds, zBounds));
            Collider[] thingsArray = Physics.OverlapSphere(spawnChecker.transform.position, 
            dangerZones[0].GetComponent<DangerZone>().GetRadius() + 3, dangerLayer);
            if (thingsArray.Length == 0)
            {
                Instantiate(dangerZones[0], spawnChecker.transform.position, Quaternion.identity);
                dangerZones.RemoveAt(0);
                done = true;
            }
        }
    }
}
