using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private float spawnTick;
    [SerializeField] GameObject[] bonusRoster;
    private Camera cam;
    private float tickValue;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update() 
    {
        if (tickValue>0)
        {
            tickValue-=Time.deltaTime;
        }
        else
        {
            SpawnBonus();
            tickValue=spawnTick;
        }
    }

    private void SpawnBonus() 
    {
        float height = cam.orthographicSize;
        float width = cam.orthographicSize * cam.aspect;
        Instantiate(bonusRoster[Random.Range(0, bonusRoster.Length)], 
        new Vector3(cam.transform.position.x + Random.Range(-width, width), 1, cam.transform.position.z + Random.Range(-height, height)), Quaternion.identity);
    }
}
