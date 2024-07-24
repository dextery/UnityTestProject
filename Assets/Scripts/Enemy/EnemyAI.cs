using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Enemy enemyStats;
    private Transform player;
    private Rigidbody rb;
    private float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemyStats = GetComponent<Enemy>();
        speed = enemyStats.GetSpeed();
    }

    private void Update()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;
        Vector3 movement = direction/distance;
        Vector3 move = new Vector3(movement.x * speed, 0, movement.z * speed);
        rb.velocity = move;
        transform.forward = move;
    }
}
