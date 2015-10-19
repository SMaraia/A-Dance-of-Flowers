using UnityEngine;
using System.Collections;

/// <summary>
/// Common component for all flowers. Handles the lifespan and death of flowers.
/// </summary>
public class AIFlower : MonoBehaviour {
    public float timeStartToDeath = 20.0f;  // LifeSpan starting from time of creation
    public float timeCaptureToDeath = 6.0f; // LifeSpan starting from time of capture

    public float remainingTime; // Time remaining in life
    public float timer;         // Time since the starting time
    public float timeStart;     // Time of creation or capture.

    private Node node;
    private FlowerAnimator flowerAnimator;

	// Use this for initialization
	void Start () {
        node = GetComponent<Node>();
        flowerAnimator = GetComponent<FlowerAnimator>();

        timeStart = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        timer = Time.time - timeStart;

        if (!node.captured)
        {
            remainingTime = timeStartToDeath - timer;
            
 

            if (remainingTime < 0)
            {
                GameObject.Destroy(gameObject);
            }
            if (remainingTime < 3)
            {
                flowerAnimator.SetAlpha(remainingTime / 3);
            }
        }
        else
        {
            remainingTime = timeCaptureToDeath - timer;

            if (remainingTime < 0)
            {
                GameObject.Destroy(gameObject);
            }
            if (remainingTime < 3)
            {
                flowerAnimator.SetAlpha(remainingTime / 3);
            }
        }
	}

    void OnCapture()
    {
        timeStart = Time.time;
    }
}
