using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PassiveItemScriptableObject", menuName = "ScriptableObjects/Passive Item")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField] private float multipler;
    public float Multipler{get => multipler; private set => multipler = value;}

    [SerializeField] private int level;
    public int Level { get => level; private set => level = value; }


    //Next passive item prefab when level up, not to confused with the item spawned at start game
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
