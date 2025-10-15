using UnityEngine;
using Unity.Netcode;

public class Attack : NetworkBehaviour
{
    private Voice voice;

    void Start()
    {
        voice = GetComponentInParent<Voice>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Don't do anything if this isn't our own object
        if (!IsOwner) return;

        // Don't hit ourselves
        if (other.transform.root == transform.root) return;

        // Look for the KnockbackReceiver on the other object
            KBR knockback = other.GetComponentInParent<KBR>();
        if (knockback != null)
        {
            float loudness = voice.loudness;
            Vector2 direction = (other.transform.position - transform.position).normalized;
            float force = loudness * 7f;

            // Send it to the server so the knockback works across the network
            ulong targetId = knockback.GetComponent<NetworkObject>().NetworkObjectId;
            ApplyKnockbackServerRpc(targetId, direction, force);
        }
    }

    [ServerRpc]
    void ApplyKnockbackServerRpc(ulong targetId, Vector2 direction, float force)
    {
        if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(targetId, out NetworkObject netObj))
        {
            KBR knockback = netObj.GetComponent<KBR>();
            if (knockback != null)
            {
                knockback.ApplyKnockback(direction, force);
            }
        }
    }
}