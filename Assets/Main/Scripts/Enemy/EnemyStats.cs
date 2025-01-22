using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyScriptableObject enemyData;
    [HideInInspector] public float currentMoveSpeed;
    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentDamage;

    public float deSpawnDistance = 20f;   //the distance enemy will de-spawn when player walk out from the enemy
    private Transform player;

    private void Awake(){
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.Health;
        currentDamage = enemyData.Damage;
    }

    private void Start() {
        player = FindObjectOfType<PlayerStats>().transform;
        
    }

    private void Update() {
        CheckDistanceFromPlayer();
    }
    public void TakeDamage(float dmg){
        currentHealth -= dmg;
        if(currentHealth <= 0){
            Kill();
        }
    }

    public void Kill(){
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player")){
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
            Debug.Log("Attacking");
        }
    }

    private void OnDestroy(){
        if(!gameObject.scene.isLoaded){
            return;
        }
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        enemySpawner.OnEnemyKilled();
    }

    private void CheckDistanceFromPlayer(){
        if(Vector2.Distance(transform.position, player.position) >= deSpawnDistance){
            ReturnEnemyClosePlayer();
        }
    }

    private void ReturnEnemyClosePlayer(){
        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + enemySpawner.relativeSpawnPoints[Random.Range(0, enemySpawner.relativeSpawnPoints.Count)].position;
    }
}
