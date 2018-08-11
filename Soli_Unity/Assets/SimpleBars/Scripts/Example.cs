using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

	//Bars being used in the scene
	[SerializeField]
	private BarType health;
	[SerializeField]
	private BarType mana;
	[SerializeField]
	private BarType super;
	[SerializeField]
	private BarType healthCircle;
	[SerializeField]
	private BarType manaCircle;
	[SerializeField]
	private BarType heartFill;

	//Call initialize for each bar type that you are using.
	void Awake()
	{
		health.Initialize();
		mana.Initialize ();
		super.Initialize ();
		healthCircle.Initialize ();
		manaCircle.Initialize ();
		heartFill.Initialize ();
	}

	//Example code to reflect changes of the bars. Keyboard input - Up/Down Arrow!
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.UpArrow)) {
			health.CurrentValue += 10;
			mana.CurrentValue += 25;
			super.CurrentValue += 12.5f;
			healthCircle.CurrentValue += 10;
			manaCircle.CurrentValue += 25;
			heartFill.CurrentValue += 50;
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) {
			health.CurrentValue -= 10;
			mana.CurrentValue -= 25;
			super.CurrentValue -= 12.5f;
			healthCircle.CurrentValue -= 10;
			manaCircle.CurrentValue -= 25;
			heartFill.CurrentValue -= 50;

		}
	}
}
