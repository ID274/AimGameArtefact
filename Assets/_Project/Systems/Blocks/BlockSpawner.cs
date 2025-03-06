using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private int blocksToSpawn;
    [SerializeField] private bool useRandomDistance = false;
    [SerializeField] private float normalDistanceFromPlayer = 20;
    [SerializeField] private float minDistanceFromPlayer = 10;
    [SerializeField] private float maxDistanceFromPlayer = 30;
    [SerializeField] private float minY = 1;
    [SerializeField] private float maxY = 7;

    [Header("References")]
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private GameObject[] blocksSpawned;

    private Dictionary<Vector3, GameObject> blocks = new Dictionary<Vector3, GameObject>();

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
            Vector3 v = GetLocationToSpawn(playerPos, block);
            if (v == Vector3.zero && !blocks.ContainsKey(Vector3.zero))
            {
                break;
            }
            block.transform.position = v;
            blocks.Add(block.transform.position, block);
        }
    }

    private Vector3 GetLocationToSpawn(Vector3 playerPos, GameObject block)
    {
        Vector3 v = GenerateNewLocation(playerPos);

        int attempts = 0;
        while (blocks.ContainsKey(v))
        {
            v = GenerateNewLocation(playerPos);
            attempts++;
            if (attempts > 100)
            {
                Destroy(block);
                return Vector3.zero;
            }
        }
        return v;
    }

    private Vector3 GenerateNewLocation(Vector3 playerPos)
    {
        float angle = Random.Range(0f, Mathf.PI * 2);
        float distance = useRandomDistance == true ? Random.Range(minDistanceFromPlayer, maxDistanceFromPlayer) : normalDistanceFromPlayer;
        float xPos = playerPos.x + Mathf.Cos(angle) * distance;
        float zPos = playerPos.z + Mathf.Sin(angle) * distance;
        float yPos = Random.Range(minY, maxY); // Generate a random Y position within the specified range
        return new Vector3(Mathf.FloorToInt(xPos), Mathf.FloorToInt(yPos), Mathf.FloorToInt(zPos));
    }
}
