using UnityEngine;

public class ChunkSpawner : MonoBehaviour
{
    public GameObject chunkPrefab;         // Prefab deines Bodenstücks
    public Transform player;               // Dein Spieler
    public float chunkWidth = 20f;         // Breite eines Chunks
    private float nextSpawnX = 0f;

    void Start()
    {
        // Spieler automatisch finden, wenn nicht gesetzt
        if (player == null)
        {
            GameObject foundPlayer = GameObject.Find("Wizard");
            if (foundPlayer != null)
            {
                player = foundPlayer.transform;
            }
            else
            {
                Debug.LogError("⚠️ Kein GameObject namens 'Wizard' gefunden!");
            }
        }

        // Ersten Chunk spawnen
        SpawnChunk();
    }

    void Update()
    {
        // Spawne neuen Chunk, wenn Spieler sich nach rechts bewegt
        if (player != null && player.position.x > nextSpawnX - (chunkWidth * 2))
        {
            SpawnChunk();
        }
    }

    void SpawnChunk()
    {
        Vector3 spawnPosition = new Vector3(nextSpawnX, 0, 0);
        Instantiate(chunkPrefab, spawnPosition, Quaternion.identity);
        nextSpawnX += chunkWidth;
    }
}
