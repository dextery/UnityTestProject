using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private DangerType dangerType;
    private float radius;

    private void Start() 
    {
        radius = gameObject.transform.localScale.x;
    }
    public enum DangerType
    {
        Death,
        Slow
    }
    public int GetDangerType()
    {
        return (int)dangerType;
    }
    public float GetRadius() 
    {
        return radius;
    }
}
