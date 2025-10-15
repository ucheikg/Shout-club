using UnityEngine;
using Unity.Netcode;

public class tracker : NetworkBehaviour
{
    [Header("Vectors")]
    private Vector2 Arrow;
    private Vector2 playerLocation;

    [Header("Transforms")]
    private Transform player;

    private GameObject AP;

    private bool OFB;

    public override void OnNetworkSpawn()
    {
        player = GetComponent<Transform>();

        if (!IsOwner)
            return;

        // Try to find the Arrow in the scene
        AP = GameObject.Find("Arrow");

        if (AP == null)
        {
            Debug.LogWarning("Arrow object not found! Make sure it's named 'Arrow' and exists in the scene.");
            return;
        }

        // Disable sprite (visual only) at start
        var spriteRenderer = AP.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
            spriteRenderer.enabled = false;
    }

    void Update()
    {
        if (!IsOwner || AP == null)
            return;

        playerLocation = new Vector2(player.position.x, player.position.y);

        if (OFB)
        {
            Arrow = AP.transform.position;
            Arrow.x = playerLocation.x;
            AP.transform.position = Arrow;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsOwner || AP == null)
            return;

        if (other.CompareTag("bounds"))
        {
            var sr = AP.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = true;
            OFB = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!IsOwner || AP == null)
            return;

        if (other.CompareTag("bounds"))
        {
            var sr = AP.GetComponent<SpriteRenderer>();
            if (sr != null) sr.enabled = false;
            OFB = false;
        }
    }
}