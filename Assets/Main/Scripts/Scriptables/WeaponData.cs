using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Data/Weapon Data")]
public class WeaponData : ScriptableObject
{
    public Sprite icon;
    public int maxLevel;

    [HideInInspector] public string behaviour;
    public Weapon.Stats baseStats;
    public Weapon.Stats[] linearUpgrade;
    public Weapon.Stats[] randomUpgrade;

    public Weapon.Stats GetLevelData(int level){
        //if level = 2, Get level data will return to linear Upgrade [0] ==> linear Upgrade increase from level 2 
        if(level - 2 < linearUpgrade.Length){
            return linearUpgrade[level - 2];
        }
        //if linearUpgrade is max, we will choose randomUpgrade
        if(randomUpgrade.Length > 0){
            return randomUpgrade[Random.Range(0,randomUpgrade.Length)];
        }
        //return empty value
        Debug.LogWarning(string.Format("Weapon doesn't have its level up stats configured for Level {0}!",level));
        return new Weapon.Stats();
    }
}
