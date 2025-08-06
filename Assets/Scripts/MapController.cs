using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player;
    public float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public GameObject currentChunk;
    PlayerMovement pm;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist; //Must be greater than the length and width of the tilemap
    float opDist;
    float optimizerCooldown;
    public float optimizerCooldownDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None)[0];


    }

    // Update is called once per frame
    void Update()
    {
        chunkChecker();
        ChunkOptimizer();
    }

    void chunkChecker()
    {
        if (!currentChunk)
        {
            return;
        }

        if (pm.moveDir.x > 0) // Moving Right
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right").position;
                spawnChunk();
            }
        }

        if (pm.moveDir.x < 0) // Moving Left
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left").position;
                spawnChunk();
            }
        }

        if (pm.moveDir.y > 0) // Moving Up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Up").position;
                spawnChunk();
            }
        }

        if (pm.moveDir.y < 0) // Moving Down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Down").position;
                spawnChunk();
            }
        }

        if (pm.moveDir.x > 0 && pm.moveDir.y > 0) // Moving Right Up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Up").position;
                spawnChunk();
            }
        }
        if (pm.moveDir.x > 0 && pm.moveDir.y < 0) // Moving Right Down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Right Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Right Down").position;
                spawnChunk();
            }
        }
        if (pm.moveDir.x < 0 && pm.moveDir.y > 0) // Moving Left Up
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Up").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Up").position;
                spawnChunk();
            }
        }
        if (pm.moveDir.x < 0 && pm.moveDir.y < 0) // Moving Left Down
        {
            if (!Physics2D.OverlapCircle(currentChunk.transform.Find("Left Down").position, checkerRadius, terrainMask))
            {
                noTerrainPosition = currentChunk.transform.Find("Left Down").position;
                spawnChunk();
            }
        }



    }

    void spawnChunk()
    {
        int rand = Random.Range(0,terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand],noTerrainPosition,Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void ChunkOptimizer()
    {
        optimizerCooldown -= Time.deltaTime;

        if (optimizerCooldown <= 0f)
        {
            optimizerCooldown = optimizerCooldownDuration;
        }
        else
        {
            return;
        }
        foreach (GameObject chunk in spawnedChunks)
        { 
            opDist = Vector3.Distance(player.transform.position, chunk.transform.position);
            if (opDist > maxOpDist) 
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
