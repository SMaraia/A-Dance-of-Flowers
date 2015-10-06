using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // radians
    float theta;
    float r;
    float velTheta;
    float velR;
    public float RSensitivity = 0.05f;
    public GameObject parent;    

	// Use this for initialization
	void Start () {
        r = this.transform.position.magnitude;
        velR = 0;
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

        velTheta += 0.005f * Input.GetAxis("Spinning");

        velTheta *= 0.97f;

        theta += velTheta;
        //theta += 0.1f;

        Debug.Log(Input.GetAxis("TetherLength"));

        velR += Input.GetAxis("TetherLength") * RSensitivity;

        r += velR;

        if (r > 5.0f)
        {
            r = 5.0f;
        }
        velR *= 0.85f;

        

        if (r < 0.5f)
            r = 0.5f;


        if (theta < 0)
        {
            theta += 2 * Mathf.PI;
        }
        else if (theta > 2 * Mathf.PI)
        {
            theta -= 2 * Mathf.PI;
        }
	}	

    //void OnTriggerEnter2D(Collider2D other) {
    //    if(other.gameObject.tag == "Flower")
    //    {
    //        Debug.Log("Captured Flower");

    //        other.gameObject.GetComponent<Flower>().Capture(this);
    //    }
    //}
}
