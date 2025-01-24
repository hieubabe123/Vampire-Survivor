using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
    [SerializeField] private List<GameObject> terrainChunks;
    [SerializeField] private GameObject player;
    public float checkerRadius;
    [SerializeField] private LayerMask terrainMask;
    public GameObject currentChunk;
    private Vector3 playerLastPosition;


    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    private GameObject latestChunk;
    public float maxOptimizeDistance; //Must be greater than length & height of tile map
    private float chunkToPlayerDistance;

    private float currentOptimizerCooldown;
    public float optimizerCooldownTime;


    private void Start()
    {
        playerLastPosition = player.transform.position;
    }

    private void Update()
    {
        ChunkChecker();
        ChunkOptimizer();
    }

    private void ChunkChecker()
    {

        if (!currentChunk)
        {
            return;
        }
        Vector3 moveDirection = player.transform.position - playerLastPosition;
        playerLastPosition = player.transform.position;

        string directionName = GetDirectionName(moveDirection);

        //To Spawn the one Chunk matching with direction Name (Ex: Up / Right/ Left/ Down/ Up + Right/....)
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(directionName).position, checkerRadius, terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(directionName).position);

            // To Check & Spawn chunk when player move to diagonal way (Ex: Move to Up + Right way -> Spawn Up right, up)
            if(directionName.Contains("Right")){
                CheckAndSpawnChunk("Right");
            }
            if(directionName.Contains("Left")){
                CheckAndSpawnChunk("Left");
            }
            if(directionName.Contains("Up")){
                CheckAndSpawnChunk("Up");
            }
            if(directionName.Contains("Down")){
                CheckAndSpawnChunk("Down");
            }
        }

    }

    private void CheckAndSpawnChunk(string direction)
    {
        if (!Physics2D.OverlapCircle(currentChunk.transform.Find(direction).position, checkerRadius, terrainMask))
        {
            SpawnChunk(currentChunk.transform.Find(direction).position);
        }
    }

    private string GetDirectionName(Vector3 direction)
    {
        direction = direction.normalized;
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        { //Go to horizontal direction than vertical direction
            if (direction.y > 0.5f) //Up + X Direction
            {
                if (direction.x > 0)
                { //Up + Right (x > 0 & y > 0)
                    return "Up Right";
                }
                else
                { //Up + Left (x < 0 & y > 0)
                    return "Up Left";
                }
            }
            else if (direction.y < -0.5f) //Down + X direction
            {
                if (direction.x > 0)
                { //Down + Right (x > 0 & y < 0)
                    return "Down Right";
                }
                else
                { //Down + Left (x < 0 & y < 0)
                    return "Down Left";
                }
            }
            else // y == 0 (X Direction)
            {
                if (direction.x > 0f)
                { // Right (y == 0 & x > 0)
                    return "Right";
                }
                else
                { // Left (y == 0 & x < 0)
                    return "Left";
                }
            }
        }
        else
        { //Go to vertical direction than horizontal direction
            if (direction.x > 0.5f)
            {
                if (direction.y > 0f)
                { //Up + Right (x > 0 & y > 0)
                    return "Up Right";
                }
                else
                { //Up + Left (x > 0 & y < 0)
                    return "Down Right";
                }
            }
            else if (direction.x < -0.5f)
            {
                if (direction.y > 0)
                { //Up + Left (x < 0 & y > 0)
                    return "Up Left";
                }
                else
                { ///Down + Left (x < 0 & y < 0)
                    return "Down Left";
                }
            }
            else
            { // Y Direction (x == 0 )
                if (direction.y > 0)
                { // Up (y > 0 & x == 0)
                    return "Up";
                }
                else
                { // Down (y > 0 & x == 0)
                    return "Down";
                }
            }
        }
    }

    private void SpawnChunk(Vector3 spawnPosition)
    {
        int randomTerrain = Random.Range(0, terrainChunks.Count - 1);
        latestChunk = Instantiate(terrainChunks[randomTerrain], spawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    private void ChunkOptimizer()
    {
        currentOptimizerCooldown -= Time.deltaTime;
        if (currentOptimizerCooldown <= 0f)
        {
            currentOptimizerCooldown = optimizerCooldownTime;
        }
        else
        {
            return;
        }
        foreach (GameObject chunk in spawnedChunks)
        {
            chunkToPlayerDistance = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (chunkToPlayerDistance > maxOptimizeDistance)
            {
                chunk.SetActive(false);
            }
            else
            {
                chunk.SetActive(true);
            }
        }
    }
}
