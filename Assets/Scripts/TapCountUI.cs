using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapCountUI : MonoBehaviour
{
    [SerializeField] float maxValue;
    [SerializeField] Slider slider;
    [SerializeField] Gradient gradient;
    [SerializeField] Image fill;
    void Start()
    {
        slider.value = 0;
        slider.maxValue = maxValue;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }



    public void Increase(){
        slider.value += 0.5f;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
}
