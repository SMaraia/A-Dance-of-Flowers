using UnityEngine;
using System.Collections;

/// <summary>
/// Moves a flower in a random direction, at a random speed
/// </summary>
public class AILanternMover : MonoBehaviour
{
    private Node node;

    public Vector2 velocity;
    private Vector2 position;
    private Vector2 prevPosition;
    public float maxSpeed = .05F;

    public float amplitude = 0;
    public float frequency = 0;

    // Use this for initialization
    void Start()
    {
        node = gameObject.GetComponent<Node>();

        //velocity = new Vector2(Random.Range(-.05F, .05F), Random.Range(-.03F, .03F));
        velocity = new Vector2(Random.Range(-1*maxSpeed, maxSpeed), Random.Range(-1*maxSpeed*.8F, maxSpeed*.8F));

        position = new Vector2(-10 + Random.Range(0, 20), -10 + Random.Range(0, 20));
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = position;
        //gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);


        if (!node.captured)
        {
            position.y = position.x + amplitude * Mathf.Sin(frequency * position.x);
            position += velocity;

        }
    }
}