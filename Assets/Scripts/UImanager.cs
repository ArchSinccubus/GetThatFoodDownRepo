using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UImanager : MonoBehaviour
{
    public Slider StomachBar;



    public static Slider StaticStomachBar;

    public void init()
    {
        UImanager.StaticStomachBar = StomachBar;
    }

    public static void FillStomachBar(float ratio)
    {
        StaticStomachBar.value = ratio;
    }
}
