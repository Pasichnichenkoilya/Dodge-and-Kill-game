using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Image image;

    Color defaultColor;

    private void Start()
    {
        defaultColor = image.color;
    }

    public void SetHealth(float health)
    {
        slider.value = health;
    }

    public void SetMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void SetImageColor(Color color)
    {
        image.color = color;
    }
    public void SetDefaultImageColor()
    {
        image.color = defaultColor;
    }
}
