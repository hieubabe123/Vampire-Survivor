using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExperienceGem : PickUp
{
    public int experienceGranted;
    public override void Collect(){
        if(hasBeenCollected){
            return;
        }else{
            base.Collect();
        }
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExperience(experienceGranted);
        Destroy(gameObject);
    }
}
