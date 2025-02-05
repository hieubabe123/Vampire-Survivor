using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponEffect: MonoBehaviour
{
    [HideInInspector] public Weapon weapon;
    [HideInInspector] public PlayerStats playerStats;

    public float GetDamage(){
        return weapon.GetDamage();
    }

}
