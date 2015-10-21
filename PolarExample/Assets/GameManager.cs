using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float fieldWidth = 100.0f;
    public float fieldHeight = 100.0f;


    public Vector2 startPoint;

    //Nodes
    public int maxSize = 200;   // The maximum number of nodes
    public int startSize = 50;  // The starting number of nodes
    public int currentSize;    // The current number of nodes in the scene

    public Node[] prefabs;  // Array of node prefabs to instantiate

    public GameObject[] nodes;  // Array of nodes in the scene

    public int playerScore = 0;

    public float updateInterval = 10.0f;    // Interval between adding new nodes.
    public float timeSinceReset = 0.0f;

    public int totalRange;  // Total size of spawn range
    public float[] spawnRates;  // Array of spawn rates.

    private Text timerText;
    private Text scoreText;

    // Use this for initialization
    void Start()
    {
        nodes = new GameObject[0];


        startPoint = new Vector2(-fieldWidth / 2, -fieldHeight / 2);

        currentSize = 0;

        timerText = GameObject.FindWithTag("TimeText").GetComponent<Text>();
        scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();

        foreach (Node prefab in prefabs)
        {
            totalRange += prefab.spawnRange;
        }

        // Calculates spawn rates for debugging purposes
        spawnRates = new float[prefabs.Length];
        for (int i = 0; i < spawnRates.Length; i++)
        {
            spawnRates[i] = prefabs[i].spawnRange / (float)totalRange;
            Debug.Log(spawnRates[i]);
        }

        AddNodesToScene(startSize);
    }

    // Update is called once per frame
    void Update()
    {
        nodes = GameObject.FindGameObjectsWithTag("Node");
        currentSize = nodes.Length;

        scoreText.text = "Score: " + playerScore;
        timerText.text = "Time Left: " + (120 - (int)Time.time);

        if (Time.time - timeSinceReset > updateInterval)
        {
            timeSinceReset += updateInterval;

            currentSize = Mathf.Min(currentSize, maxSize);

            AddNodesToScene(15);
        }

        if (Time.time > 120)
        {
            PlayerPrefs.SetInt("SCORE", playerScore);
            Application.LoadLevel("EndScene");

        }
    }

    /// <summary>
    /// Adds given number of nodes to the scene
    /// </summary>
    /// <param name="amount">The number of new nodes</param>
    void AddNodesToScene(int amount)
    {
        Debug.Log("Adding " + amount + " nodes.");

        currentSize = Mathf.Min(currentSize + amount, maxSize);


        for (int i = nodes.Length; i < currentSize; i++)
        {

            int j = Random.Range(0, totalRange);
            int spawnIndex = 0;

            while (j > prefabs[spawnIndex].spawnRange)
            {
                j -= prefabs[spawnIndex].spawnRange;

                spawnIndex++;
            }


            Node node = prefabs[spawnIndex];

            // Sets a random position for prefab.
            node.transform.position = startPoint + new Vector2(Random.Range(0, fieldWidth), Random.Range(0, fieldHeight));

            Instantiate(node);
        }

    }
}
