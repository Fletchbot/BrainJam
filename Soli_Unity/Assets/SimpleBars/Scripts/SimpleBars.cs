using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SimpleBars : MonoBehaviour {
    
	private float fillAmount;

	[SerializeField]
	private float speed = 5.0f; //Speed multiplier for lerping
	[SerializeField]
	private Image filler;       //Image of the object used for filling
    [SerializeField]
	private Text text;			//Text object if you choose to use text on your bar

	public float MaxValue { get; set; }
    public float MinValue { get; set; }

    public float Value
	{
		set
		{
			//If we have a text object update the text!
			if(text != null)
				text.text = value + "/" + MaxValue;
			
			//Set the fill amount with a value between 0.0f and 1.0f
			fillAmount = CalculateFill (value, MinValue, MaxValue, 0, 1);
		}
	}

	void Update () 
	{
		UpdateBar();
	}

	//Update the bar fill amount by lerping for smooth movement (multiplied speed).
	private void UpdateBar()
	{
		//Update only if fill amount is different from the images fill amount to save calls!
		if (fillAmount != filler.fillAmount)
			filler.fillAmount = Mathf.Lerp(filler.fillAmount, fillAmount, Time.deltaTime * speed);
	}

	//Will return a value between 0.0f and 1.0f, which will then be used to set the images fill amount.
	private float CalculateFill(float Value, float inMin, float inMax, float outMin, float outMax)
	{
		return ((outMax - outMin)*(Value - inMin)) / ((inMax - inMin) + outMin); 
	}
}
