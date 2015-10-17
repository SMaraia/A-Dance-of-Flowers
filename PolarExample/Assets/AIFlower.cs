using UnityEngine;
using System.Collections;

public class AIFlower : MonoBehaviour {
    public float timeStartToDeath = 20.0f;
    public float timeCaptureToDeath = 6.0f;

    public float remainingTime;
    public float timer;
    public float timeStart;

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
            
            if (remainingTime < 3)
            {
                flowerAnimator.SetAlpha(remainingTime / 3);
            }

            if (remainingTime < 0)
            {
                GameObject.Destroy(gameObject);
            }
        }
        else
        {
            remainingTime = timeCaptureToDeath - timer;

            if (remainingTime < 3)
            {
                flowerAnimator.SetAlpha(remainingTime / 3);
            }

            if (remainingTime < 0)
            {
                GameObject.Destroy(gameObject);
            }
        }
	}

    void OnCapture()
    {
        timeStart = Time.time;
    }
}
