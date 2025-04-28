using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapFixer : MonoBehaviour
{
    public TilemapCollider2D tilemapCollider;

    void Start()
    {
        if (tilemapCollider != null)
        {
            // Deaktivieren und wieder aktivieren, damit Collider neu gebaut wird
            tilemapCollider.enabled = false;
            tilemapCollider.enabled = true;
        }
    }
}
