using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PieMeter : MonoBehaviour {
    private float fillAmountc1, fillAmountc2;

    [SerializeField]
    private float speed = 5.0f; //Speed multiplier for lerping
    [SerializeField]
    private Image fillersemiC1; //Image of the object used for filling
    [SerializeField]
    private Image fillersemiC2;
    [SerializeField]
    private Text text;          //Text object if you choose to use text on your bar

    public float MaxValuec1 { get; set; }
    public float MinValuec1 { get; set; }

    public float MaxValuec2 { get; set; }
    public float MinValuec2 { get; set; }

    public float Valuec1
    {
        set
        {
            //If we have a text object update the text!
            if (text != null)
                text.text = value + "/" + MaxValuec1;

            //Set the fill amount with a value between 0.0f and 1.0f
            fillAmountc1 = CalculateFill(value, MinValuec1, MaxValuec1, 0, 1);
        }
    }
    public float Valuec2
    {
        set
        {
            //If we have a text object update the text!
            if (text != null)
                text.text = value + "/" + MaxValuec2;

            //Set the fill amount with a value between 0.0f and 1.0f
            fillAmountc2 = CalculateFill(value, MinValuec2, MaxValuec2, 0, 1);
        }
    }

    void Update()
    {
        UpdateBar();
    }

    //Update the bar fill amount by lerping for smooth movement (multiplied speed).
    private void UpdateBar()
    {
        //Update only if fill amount is different from the images fill amount to save calls!
        if (fillAmountc1 != fillersemiC1.fillAmount)
            fillersemiC1.fillAmount = Mathf.Lerp(fillersemiC1.fillAmount, fillAmountc1, Time.deltaTime * speed);

        if (fillAmountc2 != fillersemiC2.fillAmount)
            fillersemiC2.fillAmount = Mathf.Lerp(fillersemiC2.fillAmount, fillAmountc2, Time.deltaTime * speed);

    }

    //Will return a value between 0.0f and 1.0f, which will then be used to set the images fill amount.
    private float CalculateFill(float Value, float inMin, float inMax, float outMin, float outMax)
    {
        return ((outMax - outMin) * (Value - inMin)) / ((inMax - inMin) + outMin);
    }
}
