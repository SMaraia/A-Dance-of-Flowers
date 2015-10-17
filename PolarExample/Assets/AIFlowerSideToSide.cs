using UnityEngine;
using System.Collections;

public class AIFlowerSideToSide : MonoBehaviour
{
    private Node node;

    public float period = 4.0f;

    private Vector2 endPoint1, endPoint2;
    private float timer, interpolant;

    // Use this for initialization
    void Start()
    {
        node = gameObject.GetComponent<Node>();

        float x = Random.Range(-5, 5);
        float y = Random.Range(-5, 5);

        Vector2 startPoint = gameObject.transform.position;

        endPoint1 = startPoint + new Vector2(x, y);
        endPoint2 = startPoint + new Vector2(-x, -y);
    }

    // Update is called once per frame
    void Update()
    {
        if (!node.captured)
        {
            timer += Time.deltaTime;

            interpolant = Mathf.Sin(timer / period * .5f) / 2 + 0.5f;

            transform.position = Vector2.Lerp(endPoint1, endPoint2, interpolant);

        }
    }
}