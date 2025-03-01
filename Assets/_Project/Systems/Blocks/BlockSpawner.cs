using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int blocksToSpawn;
    [SerializeField] private float minDistanceFromPlayer = 10;
    [SerializeField] private float maxDistanceFromPlayer = 30;
    [SerializeField] private float minY = 1;
    [SerializeField] private float maxY = 7;

    [Header("References")]
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject[] blocksSpawned;

    private void OnEnable()
    {
        SceneMover.SceneMoveFinished += SpawnBlocks;
    }

    private void OnDisable()
    {
        SceneMover.SceneMoveFinished -= SpawnBlocks;
    }

    private void SpawnBlocks()
    {
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        blocksSpawned = new GameObject[blocksToSpawn];
        for (int i = 0; i < blocksToSpawn; i++)
        {
            GameObject block = Instantiate(blockPrefab, transform.position, Quaternion.identity);
            Material material = block.GetComponent<Renderer>().material;
            blocksSpawned[i] = block;
            block.transform.position = GetLocationToSpawn(playerPos);
        }
    }

    private Vector3 GetLocationToSpawn(Vector3 playerPos)
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer);
        float xPos = playerPos.x + Mathf.Cos(angle) * distance;
        float zPos = playerPos.z + Mathf.Sin(angle) * distance;
        float yPos = Random.Range(minY, maxY); // Generate a random Y position within the specified range

        return new Vector3(xPos, yPos, zPos);
    }
}
