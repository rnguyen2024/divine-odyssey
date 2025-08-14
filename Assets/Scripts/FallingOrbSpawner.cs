using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    public GameObject orbPrefab;        // Assign your orb prefab
    public Transform player;            // Player position to spawn above
    public float spawnInterval = 2f;    // Time between spawns
    public float spawnRangeX = 8f;      // Horizontal random range

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnOrb();
            timer = 0f;
        }
    }

    void SpawnOrb()
    {
        if (orbPrefab != null && player != null)
        {
            // Random X position around player
            float randomX = player.position.x + Random.Range(-spawnRangeX, spawnRangeX);

            // Spawn a bit above the player
            Vector3 spawnPos = new Vector3(randomX, player.position.y + 8f, 0f);

            Instantiate(orbPrefab, spawnPos, Quaternion.identity);
        }
    }
}
