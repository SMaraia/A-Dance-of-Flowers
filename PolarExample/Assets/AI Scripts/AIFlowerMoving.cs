using UnityEngine;
using System.Collections;

/// <summary>
/// Moves a flower in a random direction, at a random speed
/// </summary>
public class AIFlowerMoving : MonoBehaviour
{
    private Node node;

    public Vector2 velocity;
    private float rotationSpeed;
    private Vector2 position;

    // Use this for initialization
    void Start()
    {
        node = gameObject.GetComponent<Node>();

        velocity = new Vector2(Random.Range(-.07F, .07F), Random.Range(-.07F, .07F));
        rotationSpeed = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;

        position = new Vector2(-10 + Random.Range(0, 20), -10 + Random.Range(0, 20));
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = position;
        gameObject.transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed);

        if (!node.captured)
        {
            position += velocity;

        }
    }
}