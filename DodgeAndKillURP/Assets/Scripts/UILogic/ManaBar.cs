using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider slider;

    public void SetValue(float value)
    {
        slider.value = value;
    }

    public void SetMaxValue(float maxDash)
    {
        slider.maxValue = maxDash;
        slider.value = maxDash;
    }
}
