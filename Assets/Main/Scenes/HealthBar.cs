using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField, Range(0,100)] public float healthFillPerHundred;
    public float OnSliderValueChange(){
        slider.SetValueWithoutNotify(healthFillPerHundred);
        return healthFillPerHundred;
    }
}
