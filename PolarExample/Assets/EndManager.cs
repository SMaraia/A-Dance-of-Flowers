using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Text scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        scoreText.text = "Score: " + PlayerPrefs.GetInt("SCORE");
	}
	
	// Update is called once per frame
	void Update () {
	
        if(Input.GetAxis("Start") != 0)
        {
            Application.LoadLevel("Circles");
        }

	}
}
