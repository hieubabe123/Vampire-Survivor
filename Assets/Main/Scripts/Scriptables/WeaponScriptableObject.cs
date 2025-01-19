using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponScriptableObject", menuName = "ScriptableObjects/Weapon")]
public class WeaponScriptableObject : ScriptableObject
{
    [SerializeField] private GameObject weaponPrefab;

    public GameObject WeaponPrefab {get => weaponPrefab; private set => weaponPrefab = value; }

    // Base stat for weapons
    [SerializeField] private float damageWeapon;
    public float DamageWeapon { get => damageWeapon; private set => damageWeapon = value; }

    [SerializeField] private float speedWeapon;
    public float SpeedWeapon { get => speedWeapon; private set => speedWeapon = value; }

    [SerializeField]private float cooldownDuration;
    public float CooldownDuration { get => cooldownDuration; private set => cooldownDuration = value;}

    [SerializeField]private int pierce;
    public int Pierce { get => pierce; private set => pierce = value;}

    [SerializeField] private int level;
    public int Level { get => level; private set => level = value; }


    //Next Weapon prefab when level up, not to confused with the weapon spawned at start game
    [SerializeField] private GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab{ get => nextLevelPrefab; private set => nextLevelPrefab = value;}

    //Name & Description
    [SerializeField] private new string name;
    public string Name { get => name; private set => name = value; }

    [SerializeField] private string description;
    public string Description { get => description; private set => description = value; }

    //Icon
    [SerializeField] private Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }
}
