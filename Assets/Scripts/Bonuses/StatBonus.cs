using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class StatBonus : Bonus
{
    [SerializeField] private BonusType bonusType;
    [SerializeField] private float bonusDuration;
    public int GetBonusType() 
    {
        return (int)bonusType;
    }
    
    public float GetBonusDuration()
    {
        return bonusDuration;
    }

    public enum BonusType 
    {
        Invul,
        Speed
    }
}
