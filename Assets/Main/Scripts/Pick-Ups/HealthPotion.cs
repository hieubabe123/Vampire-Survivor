using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, ICollectible
{
    public int healthToStore;
    public void Collect(){
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToStore);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
