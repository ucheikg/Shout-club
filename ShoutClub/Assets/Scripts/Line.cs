using UnityEngine;

public class Line : MonoBehaviour
{
    [Header("Wave Settings")]
    [SerializeField] private int points = 100;
    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float frequency = 1f;
    [SerializeField] private float speed = 1f;

    [Header("Endpoints")]
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private LineRenderer line;
    private float timeOffset = 0f;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = points;
    }


    void Update()
    {
        
        timeOffset += Time.deltaTime * speed;

        Vector3 start = pointA.position;
        Vector3 end = pointB.position;
        Vector3 direction = end - start;
        Vector3 unitDir = direction.normalized;
        Vector3 perpendicular = Vector3.Cross(unitDir, Vector3.forward);

        for (int i = 0; i < points; i++)
        {
            float t = (float)i / (points - 1);
            Vector3 basePos = Vector3.Lerp(start, end, t);
            float wave = Mathf.Sin((t * frequency * Mathf.PI * 2f) + timeOffset) * amplitude;
            Vector3 finalPos = basePos + (perpendicular * wave);
            line.SetPosition(i, finalPos);
        }
    }
}