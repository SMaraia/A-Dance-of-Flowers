using UnityEngine;
using System.Collections;

public class FlowerAnimator : MonoBehaviour {

	private Node flower;

	public SpriteRenderer spriteRenderer;
	
	public Color currentColor = Color.white;
	public Color transitionColor = Color.white;
	public Color nextColor = Color.white;
	
	public float colorChangeDuration = 2.0f;
	public float colorChangeStartTime;
	
	public bool changingColor;

	// Use this for initialization
	void Start () {
		flower = gameObject.GetComponent<Node>();

		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.color = currentColor;
		changingColor = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (changingColor)
		{
			float colorChangeTime = Time.time - colorChangeStartTime;
			transitionColor = Color.Lerp(currentColor, nextColor, colorChangeTime / colorChangeDuration);
			
			spriteRenderer.color = transitionColor;
			
			if(colorChangeTime > colorChangeDuration)
			{
				changingColor = false;
				currentColor = nextColor;
			}
		}
		
		if (flower.captured)
		{
			gameObject.transform.Rotate (0, 0 , Time.deltaTime * 60);
		}
	}

	public void ChangeColor(Color newColor)
	{
		nextColor = newColor;
		currentColor = transitionColor;
		changingColor = true;
		colorChangeStartTime = Time.time;
	}
	
	public void ChangeColorRandom()
	{
		ChangeColor(
			new Color(
			Random.Range(0.0f,1.0f),
			Random.Range(0.0f,1.0f),
			Random.Range(0.0f,1.0f))
			);
	}

	public void OnCapture()
	{
		ChangeColor(Color.red);
	}

	public void OnNeutral()
	{
		ChangeColorRandom();
	}
}
