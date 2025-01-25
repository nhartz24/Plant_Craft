using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void setMaxHealth(int health) {
        slider.maxValue = health;
        slider.value = health;
    }

    public void setHealth(int health) {
        slider.value = health;
    }
}
