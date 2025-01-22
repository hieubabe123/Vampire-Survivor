using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    [SerializeField] public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier(){
        //the parent script to apply update stat

    }
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }
}
