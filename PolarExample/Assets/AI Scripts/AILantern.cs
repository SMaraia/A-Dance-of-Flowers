using UnityEngine;
using System.Collections;

/// <summary>
/// Common component for all lanterns. Handles the lifespan and death of lanterns.
/// </summary>
public class AILantern : MonoBehaviour {
    public float timeStartToDeath = 20.0f;  // LifeSpan starting from time of creation
    public float timeCaptureToDeath = 6.0f; // LifeSpan starting from time of capture

    public float remainingTime; // Time remaining in life
    public float timer;         // Time since the starting time
    public float timeStart;     // Time of creation or capture.

    private Node node;
    private LanternAnimator animator;

	// Use this for initialization
	void Start () {
        node = GetComponent<Node>();
        animator = GetComponent<LanternAnimator>();

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
                animator.SetAlpha(remainingTime / 3);
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
                animator.SetAlpha(remainingTime / 3);
            }
        }
	}

    void OnCapture()
    {
        timeStart = Time.time;
    }
}
