using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]
public class CharacterScriptableObject : ScriptableObject
{


    [SerializeField] private Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }

    [SerializeField] private AnimatorController animator;
    public AnimatorController Animator { get => animator; private set { animator = value; } }


    [SerializeField] private new string name;
    public string Name { get => name; private set => name = value; }

    [SerializeField] private GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    [SerializeField] private float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }

    [SerializeField] private float recovery;
    public float Recovery { get => recovery; private set => recovery = value; }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }

    [SerializeField] private float might;
    public float Might { get => might; private set => might = value; }
    [SerializeField] private float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }

    [SerializeField] private float magnet;
    public float Magnet { get => magnet; private set => magnet = value; }


}
