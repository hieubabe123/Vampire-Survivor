using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyScriptableObject", menuName = "ScriptableObjects/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [SerializeField] private float moveSpeed;
    public float MoveSpeed{get => moveSpeed; private set => moveSpeed = value;}
    [SerializeField] private float health;
    public float Health{get => health; private set => health = value;}
    [SerializeField] private float damage;
    public float Damage{get => damage; private set => damage = value;}
}
