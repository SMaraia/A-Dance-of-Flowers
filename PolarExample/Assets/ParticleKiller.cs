using UnityEngine;
using System.Collections;

public class ParticleKiller : MonoBehaviour {

    float timeStart;
	// Use this for initialization
	void Start () {
        timeStart = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - timeStart > 0.5f)
        {
            Destroy(this.gameObject);
        }
	}
}
