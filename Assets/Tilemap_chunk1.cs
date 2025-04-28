using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDebugger : MonoBehaviour
{
    public Tilemap tilemap;

    void Update()
    {
        if (tilemap == null)
        {
            Debug.LogWarning("Tilemap nicht zugewiesen!");
            return;
        }

        Vector3 playerWorldPos = GameObject.FindWithTag("Player").transform.position;
        Vector3Int cellPos = tilemap.WorldToCell(playerWorldPos);

        TileBase tile = tilemap.GetTile(cellPos);

        if (tile == null)
        {
            Debug.LogWarning("Tile unter Tilemap-Position ist NULL!");
        }
        else
        {
            Debug.Log("Tile vorhanden unter " + cellPos);
        }
    }
}
