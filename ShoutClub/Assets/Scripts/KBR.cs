using UnityEngine;

public class KBR : MonoBehaviour
{
    public Rigidbody2D rb;

    void Awake()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    public void ApplyKnockback(Vector2 direction, float force)
    {
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }
}
