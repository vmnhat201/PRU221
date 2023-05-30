using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
public class SliderData
{
    public float value;
    public float minValue;
    public float maxValue;
    public string name;
    public SliderData(Slider slider)
    {
        if (slider == null)
        {
            Debug.Log("Slider is null");
            return;
        }
        value = slider.value;
        minValue = slider.minValue;
        maxValue = slider.maxValue;
        name = slider.name;
    }
}
