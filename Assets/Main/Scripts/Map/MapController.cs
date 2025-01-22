using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrainChunks;
    [SerializeField] private GameObject player;
    public float checkerRadius;
    private Vector3 noTerrainPosition;
    [SerializeField] private LayerMask terrainMask;
    private PlayerMovement playerMovement;

    public GameObject currentChunk;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    private GameObject latestChunk;
    public float maxOptimizeDistance; //Must be greater than length & height of tile map
    private float chunkToPlayerDistance;

    private float currentOptimizerCooldown;
    public float optimizerCooldownTime;

    
    private void Start(){
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update(){
        ChunkChecker();
        ChunkOptimizer();
    }

    private void ChunkChecker(){

        if(!currentChunk){
            return;
        }

        if(playerMovement.moveDir.x > 0 && playerMovement.moveDir.y ==0){ //right
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position,checkerRadius,terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                SpawnBlock();
            }
        } else if(playerMovement.moveDir.x < 0 && playerMovement.moveDir.y ==0){ // left
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position,checkerRadius,terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                SpawnBlock();
            }
        }else if(playerMovement.moveDir.x == 0 && playerMovement.moveDir.y > 0){ //Up
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position,checkerRadius,terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                SpawnBlock();
            }
        }else if(playerMovement.moveDir.x == 0 && playerMovement.moveDir.y < 0){ //Down
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position,checkerRadius,terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                SpawnBlock();
            }
        }else if(playerMovement.moveDir.x > 0 && playerMovement.moveDir.y > 0){ //Up + Right
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up Right").position,checkerRadius,terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Up Right").position;
                SpawnBlock();
            }
        }else if(playerMovement.moveDir.x < 0 && playerMovement.moveDir.y > 0){ //Up + Left
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Up Left").position,checkerRadius,terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Up Left").position;
                SpawnBlock();
            }
        }else if(playerMovement.moveDir.x > 0 && playerMovement.moveDir.y < 0){ //Down + Right
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down Right").position,checkerRadius,terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Down Right").position;
                SpawnBlock();
            }
        }else if(playerMovement.moveDir.x < 0 && playerMovement.moveDir.y < 0){ //Down + Left
            if(!Physics2D.OverlapCircle(currentChunk.transform.Find("Down Left").position,checkerRadius,terrainMask)){
                noTerrainPosition = currentChunk.transform.Find("Down Left").position;
                SpawnBlock();
            }
        }
    }

    private void SpawnBlock(){
        int randomTerrain = Random.Range(0,terrainChunks.Count - 1);
        latestChunk = Instantiate(terrainChunks[randomTerrain ],noTerrainPosition,Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    private void ChunkOptimizer(){
        currentOptimizerCooldown -= Time.deltaTime;
        if(currentOptimizerCooldown <= 0f){
            currentOptimizerCooldown = optimizerCooldownTime;
        }else{
            return;
        }
        foreach(GameObject chunk in spawnedChunks){
            chunkToPlayerDistance = Vector3.Distance(player.transform.position, chunk.transform.position);
            if(chunkToPlayerDistance > maxOptimizeDistance){
                chunk.SetActive(false);
            }else{
                chunk.SetActive(true);
            }
        }
    }
}
