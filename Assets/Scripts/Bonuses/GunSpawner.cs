using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTick;
    [SerializeField] GameObject[] gunRoster;
    private Camera cam;
    private float tickValue;
    PlayerScript player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
        cam = Camera.main;
    }

    void Update()
    {
        if (tickValue>0)
        {
            tickValue-=Time.deltaTime;
        }
        else
        {
            SpawnGun();
            tickValue=spawnTick;
        }
    }

    private void SpawnGun() 
    {
        int index=0;
        int currGun = player.GetCurrentWeapon();
        bool done = false;
        while (!done) 
        {
            index = Random.Range(0, gunRoster.Length);
            if (index!=currGun)
            {
                done=true;
            }
        }
        float height = cam.orthographicSize;
        float width = cam.orthographicSize * cam.aspect;
        Instantiate(gunRoster[index], 
        new Vector3(cam.transform.position.x + Random.Range(-width, width), 1, 
        cam.transform.position.z + Random.Range(-height, height)), Quaternion.identity); 
    }
}
