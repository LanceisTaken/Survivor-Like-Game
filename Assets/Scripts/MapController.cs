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
    PlayerMovement pm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        pm = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None)[0];


    }

    // Update is called once per frame
    void Update()
    {
        chunkChecker();
    }

    void chunkChecker()
    {
        if (pm.moveDir.x > 0) // Moving Right
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(24, 0, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(24, 0, 0);
                spawnChunk();
            }
        }

        if (pm.moveDir.x < 0) // Moving Left
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(-24, 0, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(-24, 0, 0);
                spawnChunk();
            }
        }

        if (pm.moveDir.y > 0) // Moving Up
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(0, 14, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(0, 14, 0);
                spawnChunk();
            }
        }

        if (pm.moveDir.y < 0) // Moving Down
        {
            if (!Physics2D.OverlapCircle(player.transform.position + new Vector3(0, -14, 0), checkerRadius, terrainMask))
            {
                noTerrainPosition = player.transform.position + new Vector3(0, -14, 0);
                spawnChunk();
            }
        }


    }

    void spawnChunk()
    {
        int rand = Random.Range(0,terrainChunks.Count);
        Instantiate(terrainChunks[rand],noTerrainPosition,Quaternion.identity);
    }
}
