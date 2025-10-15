using UnityEngine;
using Unity.Netcode;

public class Voice : NetworkBehaviour
{
    private Rigidbody2D rb;
    public playerStats ps;
    private Aim aim;
    public AudioSource source;
    public ALD detector;

    [SerializeField] private ParticleSystem shockwave;
    [SerializeField] private GameObject effect;

    public float lS = 100;
    public float threshold = 0.5f;
    public float loudness;

    public override void OnNetworkSpawn()
    {
        rb = GetComponent<Rigidbody2D>();
        ps = GetComponent<playerStats>();
        aim = GetComponent<Aim>();
    }

    void Update()
    {
        if (!IsOwner) return;

        loudness = detector.GLFM() * lS;

        if (loudness < threshold)
            loudness = 0;

        if (loudness == 0 && shockwave.isPlaying)
            effect.SetActive(false);

        if (loudness > 0)
            effect.SetActive(true);

        rb.AddForce(-aim.direction * ps.power * loudness / 10f, ForceMode2D.Impulse);
    }
}