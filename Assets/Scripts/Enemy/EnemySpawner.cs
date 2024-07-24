using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTick;
    [SerializeField] private float difficultyTimer = 10;
    [SerializeField] GameObject[] enemyRoster;
    private Camera cam;

    private float tickValue;
    private float timeTick;   

    private float accumulatedWeights;
    private System.Random rand = new System.Random();

    private void Awake()
    {
        CalculateEnemyWeights();
    }
    void Start()
    {
        tickValue = spawnTick;
        timeTick = difficultyTimer;
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Спавн врагов
        if (tickValue>0)
        {
            tickValue-=Time.deltaTime;
        }
        else
        {
            SpawnEnemy();
            tickValue=spawnTick;
        }

        //Увеличение сложности
        if (timeTick > 0) 
        {
            timeTick-=Time.deltaTime;    
        }
        else
        {
            IncreaseDifficulty();
        }
    }

    private void SpawnEnemy()
    {
        int randomEnemy = GetRandomEnemyIndex();
        float height = cam.orthographicSize + 2;
        float width = cam.orthographicSize * cam.aspect + 2;
        Instantiate(enemyRoster[randomEnemy], 
        new Vector3(cam.transform.position.x + Random.Range(-width-10, width+10), 1, 
        cam.transform.position.z + Random.Range(-height-10, height+10)), 
        Quaternion.identity);
    }
    private void IncreaseDifficulty()
    {
        if (spawnTick > 0.5) 
        {
            spawnTick -= 0.1f;
            timeTick = difficultyTimer;
        }
    }

    private int GetRandomEnemyIndex()
    {
        double r = rand.NextDouble() * accumulatedWeights;
        for (int i=0; i<enemyRoster.Length; i++)
        {
            if(enemyRoster[i].GetComponent<Enemy>().GetWeight() >= r)
            {
                return i;
            }
        }
        return 0;
    }

    private void CalculateEnemyWeights()
    {
        accumulatedWeights=0f;
        foreach(GameObject prefab in enemyRoster)
        {
            accumulatedWeights+=prefab.GetComponent<Enemy>().GetSpawnChance();
            prefab.GetComponent<Enemy>().SetWeight(accumulatedWeights);
        }
    }
}
