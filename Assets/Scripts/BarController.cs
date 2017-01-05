using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    private float fillAmount;
    // Rate the hp decreases
    public float lerpSpeed;
    public Image image;
    public Text text;

    public float MaxValue { get; set; }
    
    public float Value
    {
        set
        {
            // Gonna break up Health and 100 from the Health : 100
            string[] tmp = text.text.Split(':');
            text.text = tmp[0] + ": " + value;
            fillAmount = Map(value, 0, MaxValue, 0, 1); 
        }
    }

    private void HandleBar()
    {
        if (fillAmount != image.fillAmount)
        {
            image.fillAmount = Mathf.Lerp(image.fillAmount, fillAmount, Time.deltaTime * lerpSpeed);
        }
    }

    // passed in value, 0 as min hp, Max HP, 0 as min and 1 as max
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
	// Update is called once per frame
	void Update ()
    {
        HandleBar();
	}
}
