using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class BarType
{
	[SerializeField]
	private SimpleBars bar;
	[SerializeField]
	private float maxVal = 100.0f;		//Max value of the bar
	[SerializeField]
	private float currentVal = 100.0f;	//Current value of the bar

	//Getter and setter for maxVal
	public float MaxVal
	{
		get 
		{
			return maxVal;
		}
		set 
		{
			this.maxVal = value;
			bar.MaxValue = maxVal;
		}
	}

	//Getter and setter for currentVal
	public float CurrentValue
	{
		get
		{
			return currentVal;
		}
		set
		{
			this.currentVal = Mathf.Clamp(value, 0, MaxVal);
			bar.Value = currentVal;
		}
	}

	//Call Initialize() in the Awake() of your player class for each bar that you are using.
	//See Example.cs for example code!
	public void Initialize()
	{
		this.MaxVal = maxVal;
		this.CurrentValue = currentVal;
	}

}
