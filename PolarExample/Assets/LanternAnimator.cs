using UnityEngine;
using System.Collections;

public class LanternAnimator : MonoBehaviour {


	public SpriteRenderer spriteRenderer;
    public SpriteRenderer reflectionRenderer;
	
	public Color currentColor = new Color(1, 1, 1, 0);
    public Color transitionColor = new Color(1, 1, 1, 0);
	public Color nextColor = Color.white;
	
	public float colorChangeDuration = 2.0f;
	public float colorChangeStartTime;
	
	public bool changingColor;

	// Use this for initialization
	void Start () {

		spriteRenderer = GetComponent<SpriteRenderer>();
        reflectionRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

		spriteRenderer.color = currentColor;
        reflectionRenderer.color = currentColor;

        ChangeColor(Color.white);

	}
	
	// Update is called once per frame
	void Update () {
		if (changingColor)
		{
			float colorChangeTime = Time.time - colorChangeStartTime;
			transitionColor = Color.Lerp(currentColor, nextColor, colorChangeTime / colorChangeDuration);

            spriteRenderer.color = transitionColor;
            reflectionRenderer.color = transitionColor;
			
			if(colorChangeTime > colorChangeDuration)
			{
				changingColor = false;
				currentColor = nextColor;
			}
		}
		
	}

	public void ChangeColor(Color newColor)
	{
		nextColor = newColor;
		currentColor = transitionColor;
		changingColor = true;
		colorChangeStartTime = Time.time;
	}

	public void OnCapture()
	{
       ChangeColor(Color.yellow);
	}


    // Manually sets the alpha of the sprite. Useful for fading in and out without effect color transitions.
    public void SetAlpha(float alpha)
    {
        spriteRenderer.color = new Color(transitionColor.r, transitionColor.g, transitionColor.b, transitionColor.a * alpha);
        reflectionRenderer.color = new Color(transitionColor.r, transitionColor.g, transitionColor.b, transitionColor.a * alpha);
    }
}
