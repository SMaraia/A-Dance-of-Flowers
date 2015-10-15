using UnityEngine;
using System.Collections;

public class Flower : MonoBehaviour {

	public bool captured = false;

	private GameManager gameManager;
	private FlowerAnimator animator;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager>();
		animator = GetComponent<FlowerAnimator>();
	}
	
	// Update is called once per frame
	void Update () {

	}


	public void Capture()
	{
		if(!captured)
		{
			gameManager.playerScore++;
			captured = true;

			animator.OnCapture();
		}
	}

	public void setNeutral()
	{
		if(captured)
		{
			captured = false;
			animator.OnNeutral();
		}
	}
}
