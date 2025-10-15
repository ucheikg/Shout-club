using UnityEngine;
using System.Collections;

public class Barries : MonoBehaviour
{
    float speed;
    bool top;
    [SerializeField] private Transform TB;
    [SerializeField] private Transform BB;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (top == false)
        {
            StartCoroutine(Up());
        }
        if (top == true){
            StartCoroutine(Down());
        }
    }

    IEnumerator Up()
    {
        Vector2 tb = new Vector2(TB.position.x, TB.position.y);
        speed = Random.Range(1f, 5f);
        var distance = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, tb,  distance);
        yield break;
    }
    IEnumerator Down()
    {
        Vector2 bb = new Vector2(BB.position.x, BB.position.y);
        speed = Random.Range(1f, 5f);
        var distance = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, bb, distance);
        yield break;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("TB"))
        {
            top = true;
        }
        if (other.gameObject.CompareTag("BB"))
        {
            top = false;
        }
    }
}
