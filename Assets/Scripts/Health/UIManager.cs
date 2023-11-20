using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Slider oxygenBar;
    public Slider foodBar;
    public Slider waterBar;

    public void SetOxygenBar(float value)
    {
        oxygenBar.value = value;
    }

    public void SetFoodBar(float value)
    {
        foodBar.value = value;
    }

    public void SetWaterBar(float value)
    {
        waterBar.value = value;
    }
}
