using UnityEngine;
using System.Collections;

public class Planet : MonoBehaviour {

    Vector2 vel;
    public float velSensitivity = 0.005f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        vel.x += velSensitivity * Input.GetAxis("Horizontal");
        vel.y += velSensitivity * Input.GetAxis("Vertical");

        transform.position = (Vector2)transform.position + vel;

        vel *= .97f;

	}
}
