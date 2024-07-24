using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private float decayTime;

    void Update()
    {
        if (decayTime > 0) 
        {
            decayTime-=Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    
}
