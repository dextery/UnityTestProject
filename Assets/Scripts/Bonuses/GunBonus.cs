using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBonus : Bonus
{
    [SerializeField] private GunType gunType;

    public int GetGunType()
    {
        return (int)gunType;
    }

    public enum GunType {
        AssaultRifle = 1,
        Shotgun,
        GrenadeLauncher
    }
}
