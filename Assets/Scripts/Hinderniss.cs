using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class TilemapTrigger : MonoBehaviour
{
    public GameObject tilemapChunk;
    public Tilemap tilemap;
    public TilemapCollider2D tilemapCollider;

    private bool hasActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasActivated) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered the trigger!");

            // Chunk aktivieren
            tilemapChunk.SetActive(true);

            // Tilemap aktualisieren (optional, aber nützlich)
            tilemap.CompressBounds();

            // Collider reaktivieren (erzeugt Composite Collider neu)
            tilemapCollider.enabled = false;
            tilemapCollider.enabled = true;

            hasActivated = true;

            // Starte Coroutine für Physik und Tile-Check
            StartCoroutine(WaitForTilemapAndEnablePhysics(other.gameObject));
        }
    }

    IEnumerator WaitForTilemapAndEnablePhysics(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 oldVelocity = rb.velocity;
            rb.simulated = false;

            // BESSER: Zwei Frames warten statt fixer Zeit
            yield return null;
            yield return null;

            rb.simulated = true;
            rb.velocity = oldVelocity;
        }

        // TILE-CHECK UNTER SPIELERPOSITION
        Vector3 checkPos = player.transform.position + Vector3.down * 0.5f;
        Vector3Int cellPos = tilemap.WorldToCell(checkPos);
        var tile = tilemap.GetTile(cellPos);

        if (tile == null)
        {
            Debug.LogWarning($"[WARN] Kein Tile unter {cellPos}! Spielerposition: {player.transform.position}");
        }
        else
        {
            Debug.Log($"✅ Tile vorhanden unter {cellPos}");
        }
    }
}
