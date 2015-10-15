using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public  float fieldWidth = 100.0f;
	public  float fieldHeight = 100.0f;

	public Vector2 startPoint;

	//Flowers
	public int maxSize = 200;
	public int startSize = 50;
	public int currentSize;
	public Flower flower;
	public Flower[] flowers;

	public int playerScore = 0;

	public float gameLength = 60.0f;
	public float resetInterval = 10.0f;
	public float timeSinceReset = 0.0f;

	// Use this for initialization
	void Start () {
		flowers = new Flower[maxSize];


		startPoint = new Vector2(-fieldWidth/2, -fieldHeight/2);

		for (int i = 0; i < startSize; i++)
		{
			Flower f = flower;

			f.transform.position = startPoint + new Vector2(Random.Range(0, fieldWidth), Random.Range(0,fieldHeight));

			flowers[i] = Instantiate(flower);

		}

		currentSize = startSize;

		for (int i = 0; i < currentSize / 4 ; i++)
		{
			if(i < startSize / 8)
			{
				flowers[i].gameObject.AddComponent<FlowerMover>().movementMode = MovementMode.ROTATE;
			}
			else
			{
				flowers[i].gameObject.AddComponent<FlowerMover>().movementMode = MovementMode.SIDETOSIDE;
			}
		}

		/*for(int i = 0; i < gridWidth; i++)
		{
			for(int j = 0; j < gridHeight; j++)
			{
				Flower f = flower;

				flower.transform.position = new Vector3(i * distance - distance * gridWidth /2, j * distance- distance * gridHeight /2, 0 );

				flowers[i * gridWidth + j] = Instantiate(flower);
			}
		}*/

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - timeSinceReset > resetInterval)
		{
			timeSinceReset += resetInterval;

			for(int i = 0; i < currentSize; i++)
			{
				flowers[i].setNeutral();
			}
		}
	}

	void OnGUI ()
	{
		GUI.Label(new Rect(0,0,128,64), "Score: " + playerScore);
		GUI.Label(new Rect(64,0,128,64), "Time: " +   Time.time );
	}
}
