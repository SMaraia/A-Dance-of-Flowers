using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int gridWidth = 10;
	public int gridHeight = 10;

	public float distance = 2.0f;

	public Flower flower;
	public Flower[] flowers;

	public int playerScore = 0;

	public float gameLength = 60.0f;
	public float resetInterval = 10.0f;
	public float timeSinceReset = 0.0f;

	// Use this for initialization
	void Start () {
		flowers = new Flower[gridWidth * gridHeight];

		for(int i = 0; i < gridWidth; i++)
		{
			for(int j = 0; j < gridHeight; j++)
			{
				Flower f = flower;

				flower.transform.position = new Vector3(i * distance - distance * gridWidth /2, j * distance- distance * gridHeight /2, 0 );

				flowers[i * gridWidth + j] = Instantiate(flower);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if(Time.time - timeSinceReset > resetInterval)
		{
			timeSinceReset += resetInterval;

			foreach(Flower f in flowers)
			{
				f.setNeutral();
			}
		}
	}

	void OnGUI ()
	{
		GUI.Label(new Rect(0,0,128,64), "Score: " + playerScore);
		GUI.Label(new Rect(64,0,128,64), "Time: " +   Time.time );
	}
}
