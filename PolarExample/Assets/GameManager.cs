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

	public Node[] prefabs;


	public GameObject[] nodes;

	public int playerScore = 0;

	public float gameLength = 60.0f;
	public float resetInterval = 10.0f;
	public float timeSinceReset = 0.0f;

	// Use this for initialization
	void Start () {
        nodes = new GameObject[maxSize];


		startPoint = new Vector2(-fieldWidth/2, -fieldHeight/2);

		for (int i = 0; i < startSize; i++)
		{

            int j = Random.Range(0, prefabs.Length);

            Node node = prefabs[j];


			node.transform.position = startPoint + new Vector2(Random.Range(0, fieldWidth), Random.Range(0,fieldHeight));

            Instantiate(node);

		}
	}
	
	// Update is called once per frame
	void Update () {
        nodes = GameObject.FindGameObjectsWithTag("Node");

		if(Time.time - timeSinceReset > resetInterval)
		{
			timeSinceReset += resetInterval;

			for(int i = 0; i < currentSize; i++)
			{
				 nodes[i].GetComponent<Node>().setNeutral();
			}
		}
	}

	void OnGUI ()
	{
		GUI.Label(new Rect(0,0,128,64), "Score: " + playerScore);
		GUI.Label(new Rect(64,0,128,64), "Time: " +   Time.time );
	}
}
