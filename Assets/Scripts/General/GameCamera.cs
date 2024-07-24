using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameCamera : MonoBehaviour
{
    private Vector3 cameraTarget;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private BoxCollider cameraBoundsLeft;
    [SerializeField] private BoxCollider cameraBoundsRight;
    [SerializeField] private BoxCollider cameraBoundsUp;
    [SerializeField] private BoxCollider cameraBoundsDown;

    Vector3 min, max; //границы

    void Start()
    {
        //Альтернатива
        //playerPosition=GameObject.FindGameObjectWithTag("Player").transform;
        min.x = cameraBoundsDown.bounds.max.x;
        max.x = cameraBoundsUp.bounds.max.x;

        min.z = cameraBoundsLeft.bounds.max.z;
        max.z = cameraBoundsRight.bounds.max.z;
    }

    void Update()
    {
        cameraTarget = new Vector3 (playerPosition.position.x, transform.position.y, playerPosition.position.z);
        Vector3 clampPos = new Vector3(
            Mathf.Clamp(cameraTarget.x, min.x, max.x),
            cameraTarget.y,
            Mathf.Clamp(cameraTarget.z, min.z, max.z)
        );

        transform.position = Vector3.Lerp(transform.position, clampPos, Time.deltaTime * 10);
    }
}
