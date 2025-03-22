using UnityEngine;
using UnityEngine.Tilemaps;

public class HiddenPlatform : MonoBehaviour
{
    private Tilemap tilemap;
    private bool isVisible = false;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        tilemap.color = new Color(1f, 1f, 1f, 0f); // Tilemap'i ba�lang��ta g�r�nmez yap
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isVisible)
        {
            RevealTilemap();
        }
    }

    void RevealTilemap()
    {
        isVisible = true;
        tilemap.color = new Color(1f, 1f, 1f, 1f); // Tilemap'i tamamen g�r�n�r yap
    }
}
