using UnityEngine;
using System.Collections;

/// <summary>
/// Rotates the Flower Node around a point
/// </summary>
public class AIFlowerRotate : MonoBehaviour
{
    private Node node;

    public float speed = 0.1f;
    public Vector2 rotationPoint = new Vector2(0, 0);

    private Vector3 rotationAxis = new Vector3(0, 0, 1);


    // Use this for initialization
    void Start()
    {
        node = gameObject.GetComponent<Node>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!node.captured)
        {
            transform.RotateAround(rotationPoint, rotationAxis, speed);
        }
    }
}
