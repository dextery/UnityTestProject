using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Actor
{
    [SerializeField] private float spawnChance;
    [SerializeField] private float pointReward;
    
    private float weight;
    private PlayerScript player;

    private void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
    }

    public override void Die()
    {
        player.AddScore(pointReward);
        base.Die();
    }

    public float GetSpawnChance()
    {
        return spawnChance;
    }

    public void SetWeight(float value)
    {
        weight=value;
    }
    public float GetWeight()
    {
        return weight;
    }
}
