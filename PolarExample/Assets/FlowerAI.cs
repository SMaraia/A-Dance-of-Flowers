using UnityEngine;
using System.Collections;

public enum MovementMode
{
	ROTATE, SIDETOSIDE
}

public class FlowerAI : MonoBehaviour {
	private Node flower;

	public MovementMode movementMode;

	public float speed = 0.1f;
	public Vector2 rotationPoint = new Vector2(0, 0);

	private Vector3 rotationAxis = new Vector3(0, 0, 1);

	private float angle = 0.0f;

	private Vector2 endPoint1, endPoint2;
	private float timer, interpolant;

	// Use this for initialization
    void Start()
    {
        flower = gameObject.GetComponent<Node>();



        if (Random.Range(0, 1) == 1)
            movementMode = MovementMode.ROTATE;
        else
            movementMode = MovementMode.SIDETOSIDE;

        float x = Random.Range(-5, 5);
        float y = Random.Range(-5, 5);

        Vector2 startPoint = gameObject.transform.position;

        endPoint1 = startPoint + new Vector2(x, y);
        endPoint2 = startPoint + new Vector2(-x, -y);

    }
	
	// Update is called once per frame
	void Update () {
		switch(movementMode)
		{	
		case MovementMode.ROTATE:
			if(!flower.captured)
			{
				transform.RotateAround(rotationPoint, rotationAxis, speed);
			}
			break;
		case MovementMode.SIDETOSIDE:
			if(!flower.captured)
			{
				timer += Time.deltaTime;

				interpolant = Mathf.Sin(timer * .5f)/2 + 0.5f;

				transform.position = Vector2.Lerp(endPoint1, endPoint2, interpolant);

			}
			break;
		}
	}
}
