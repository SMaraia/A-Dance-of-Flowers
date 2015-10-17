using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public float fieldWidth = 100.0f;
    public float fieldHeight = 100.0f;

    public Vector2 startPoint;

    //Flowers
    public int maxSize = 200;
    public int startSize = 50;
    public int currentSize;

    public Node[] prefabs;

    public GameObject[] nodes;

    public int playerScore = 0;

    public float gameLength = 60.0f;
    public float updateInterval = 10.0f;
    public float timeSinceReset = 0.0f;

    // Use this for initialization
    void Start()
    {
        nodes = new GameObject[0];


        startPoint = new Vector2(-fieldWidth / 2, -fieldHeight / 2);

        currentSize = startSize;

        AddNodesToScene(15);
    }

    // Update is called once per frame
    void Update()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        currentSize = nodes.Length;

        if (Time.time - timeSinceReset > updateInterval)
        {
            timeSinceReset += updateInterval;

            currentSize = Mathf.Min(currentSize, maxSize);

            AddNodesToScene(15);
        }
    }

    void AddNodesToScene(int amount)
    {
        Debug.Log("Adding " + amount + " nodes.");

        currentSize += amount;

        currentSize = Mathf.Min(currentSize, maxSize);

        for (int i = nodes.Length; i < currentSize; i++)
        {
            int j = Random.Range(0, prefabs.Length);

            Node node = prefabs[j];

            node.transform.position = startPoint + new Vector2(Random.Range(0, fieldWidth), Random.Range(0, fieldHeight));

            Instantiate(node);

        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 128, 64), "Score: " + playerScore);
        GUI.Label(new Rect(Screen.width - 128, 0, 128, 64), "Time: " + Time.time);
    }
}
