using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{

    [System.Serializable]
    public class Wave{ //1 wave in total waves
        public string waveName; 
        public List <EnemyGroup> enemyGroups; //a List of group of enemy in this wave
        public int waveQuota; //Count All the enemy will spawn in this wave
        public float spawnInterval; //time to spawn enemy
        public int spawnCount; // the numbers of enemies had already been spawned in this wave
    }

    [System.Serializable]
    public class EnemyGroup{ //1 group in wave
        public string enemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefabs;
    }
    public List<Wave> waves;
    public int currentWaveCount; //current wave in scene

    [Header("Spawner Attributes")]
    private float spawnTimer; //Time to determine when to spawn next enemy
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false;
    public float waveInterval; //The Interval time between each way

    [SerializeField] private bool isWaveActive = false;


    [Header("Spawn Position")]
    public List<Transform> relativeSpawnPoints;
    

    private Transform player;

    private void Start(){
        player = FindObjectOfType<PlayerStats>().transform;
        CaculateWaveQuota();
    }

    private void Update(){
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive){
            isWaveActive = true;
            StartCoroutine(BeginNextWave());
        }
        CheckTimeToSpawn();
    }

    IEnumerator BeginNextWave(){
        isWaveActive = true;
        yield return new WaitForSeconds(waveInterval);
        if(currentWaveCount < waves.Count - 1){
            isWaveActive = false;
            currentWaveCount++;
            CaculateWaveQuota();
        }
    }

    private void CaculateWaveQuota(){ //Caculate All the enemy from each waves --> currentWaveQuota = All of enemyCount in Waves
        int currentWaveQuota = 0;
        foreach(var enemyGroup in waves[currentWaveCount].enemyGroups){
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);

    }


    //Create Spawner to spawn Enemy randomly
    private void SpawnEnemies(){
        if(waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached){
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups){
                if(enemyGroup.spawnCount < enemyGroup.enemyCount){
                    if(enemiesAlive >= maxEnemiesAllowed){
                        maxEnemiesReached = true;
                        return;
                    }
                    
                    // Spawn Enemies at random position
                    Instantiate(enemyGroup.enemyPrefabs,player.position + relativeSpawnPoints[Random.Range(0, relativeSpawnPoints.Count)].position,Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }

    }

    public void OnEnemyKilled(){
        enemiesAlive--;
        
        if(enemiesAlive < maxEnemiesAllowed){
            maxEnemiesReached = false;
        }
    }

    private void CheckTimeToSpawn(){
        spawnTimer += Time.deltaTime;

        if(spawnTimer >= waves[currentWaveCount].spawnInterval){
            spawnTimer = 0;
            SpawnEnemies();
        }
    }
}
