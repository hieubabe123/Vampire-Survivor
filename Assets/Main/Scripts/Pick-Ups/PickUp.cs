using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour, ICollectible
{
    protected bool hasBeenCollected = false;

    public virtual void Collect(){
        hasBeenCollected = true;
    }


    private void OnTriggerEnter2D(Collider2D other){
        if(other.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
