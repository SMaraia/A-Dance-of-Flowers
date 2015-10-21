using UnityEngine;
using System.Collections;

/// <summary>
/// Handles whether an object is captured or not.
/// </summary>
public class Node : MonoBehaviour {

    public int score = 10;  // The score total you get when capturing the node.

	public bool captured = false;

    /// <summary>
    /// Determines the frequency of the node being spawned. Higher values means increased spawn rate.
    /// </summary>
    public int spawnRange = 10;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void Capture()
	{
		if(!captured)
		{
			gameManager.playerScore += score;
			captured = true;

            gameObject.SendMessage("OnCapture");
		}
	}

	public void setNeutral()
	{
		if(captured)
		{
			captured = false;

            gameObject.SendMessage("OnNeutral");
		}
	}
}
