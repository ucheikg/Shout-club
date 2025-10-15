using UnityEngine;
using Unity.Netcode;

public class Aim : NetworkBehaviour
{
    [SerializeField] private Transform Head;
    public Vector2 direction;
    public Vector3 MP;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
        {
            return;
        }
        Mousetrack();
    }

    void Mousetrack()
    {
        MP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        direction = (MP - Head.position).normalized;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        Head.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
