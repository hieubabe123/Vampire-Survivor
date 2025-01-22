using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats playerStats;
    [SerializeField] private CircleCollider2D playerCollector;
    [SerializeField] private float pullSpeed;


    private void Start(){
        playerStats = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();

    }

    private void Update(){
        playerCollector.radius = playerStats.CurrentMagnet;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.TryGetComponent(out ICollectible collectible)){
            //Add Force that make Collectable will pull to the player
            Rigidbody2D rb = other.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = (transform.position - other.transform.position).normalized;
            rb.AddForce(forceDirection * pullSpeed);
            collectible.Collect();
        }
    }
}
