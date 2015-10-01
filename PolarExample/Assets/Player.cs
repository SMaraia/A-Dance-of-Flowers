using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // radians
    float theta;
    float r;
    float velTheta;
    public GameObject parent;    

	// Use this for initialization
	void Start () {
        r = this.transform.position.magnitude;
        theta = Mathf.Acos(this.transform.position.x / r);
	}
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKey(KeyCode.A))
        //{
        //    this.transform.position = new Vector2(transform.position.x - .1f, transform.position.y);
        //}
        //if (Input.GetKey(KeyCode.D))
        //{
        //    this.transform.position = new Vector2(transform.position.x + .1f, transform.position.y);
        //}
        //if (Input.GetKey(KeyCode.W))
        //{
        //    this.transform.position = new Vector2(transform.position.x, transform.position.y + .1f);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    this.transform.position = new Vector2(transform.position.x, transform.position.y - .1f);
        //}

        this.transform.position = new Vector2(r * Mathf.Cos(theta), r * Mathf.Sin(theta)) + (Vector2)parent.transform.position;

        if (Input.GetKey(KeyCode.A))
        {
            velTheta += 0.005f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            velTheta -= 0.005f;
        }
        velTheta *= 0.97f;

        theta += velTheta;
        //theta += 0.1f;

        if (Input.GetKey(KeyCode.W))
        {
            r += 0.05f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            r -= 0.05f;
        }
        if (r <= 0)
            r = 0.0f;


        if (theta < 0)
        {
            theta += 2 * Mathf.PI;
        }
        else if (theta > 2 * Mathf.PI)
        {
            theta -= 2 * Mathf.PI;
        }
	}
}
