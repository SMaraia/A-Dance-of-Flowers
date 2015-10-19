using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    // radians
    float theta;
    float r;
    float velTheta;
    float velR;
    public float RSensitivity = 0.05f;
    float prevAngle;
    public GameObject parent;    

	// Use this for initialization
	void Start () {
        r = this.transform.position.magnitude;
        velR = 0;
        theta = Mathf.Acos(this.transform.position.x / r);
        prevAngle = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {

        #region Legacy Keyboard movement
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
        #endregion

        this.transform.position = new Vector2(r * Mathf.Cos(theta), r * Mathf.Sin(theta)) + (Vector2)parent.transform.position;

        Vector2 spinStickRaw = new Vector2(Input.GetAxis("SpinStickHorizontal"), Input.GetAxis("SpinStickVertical"));

        float spinStickAngle = Vector2.Angle(new Vector2(0,1), spinStickRaw);

        if (Vector2.Dot(new Vector2(1, 0), spinStickRaw) > 0)
        {
            spinStickAngle = 360.0f - spinStickAngle;
        }

        float deltaAngle = (spinStickAngle - prevAngle);

        Debug.Log("Right Stick: " + spinStickRaw.x +", " + spinStickRaw.y);

        Debug.Log("Angle: " + spinStickAngle); 

        prevAngle = spinStickAngle;
        Debug.Log("deltaAngle: " + deltaAngle);

        if (deltaAngle < 0.0f &&  spinStickRaw.magnitude > 0.85)
        {
            velTheta -= 0.005f;
        }
        else if (deltaAngle > 0.0f && spinStickRaw.magnitude > 0.85)
        {
            velTheta += 0.005f;
        }

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

    public Vector2 CentriForce(Vector2 centerPos)
    {
        Vector2 returnForce = new Vector2(centerPos.x - transform.position.x, centerPos.y - transform.position.y);

        returnForce *= -Mathf.Abs(velTheta * 0.05f);

        return returnForce;
    }

    //void OnTriggerEnter2D(Collider2D other) {
    //    if(other.gameObject.tag == "Flower")
    //    {
    //        Debug.Log("Captured Flower");

    //        other.gameObject.GetComponent<Flower>().Capture(this);
    //    }
    //}
}
