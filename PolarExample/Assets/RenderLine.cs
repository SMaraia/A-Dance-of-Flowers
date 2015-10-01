using UnityEngine;
using System.Collections;

public class RenderLine : MonoBehaviour {

    public GameObject otherPos;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        this.GetComponent<LineRenderer>().SetPosition(0, this.transform.position);
        this.GetComponent<LineRenderer>().SetPosition(1, otherPos.transform.position);
	}
}
