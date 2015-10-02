using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    Vector2 vel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A)) 
        {
            vel.x -= 0.005f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            vel.x += 0.005f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            vel.y += 0.005f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            vel.y -= 0.005f;
        }

        transform.position = (Vector2)transform.position + vel;

        vel *= .97f;

	}
}
