using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class StomachImageController : MonoBehaviour
{
    public Sprite[] StomachSprites;
    public Image StomachImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeImage(float range)
    {
        int index = Convert.ToInt32(Math.Floor(range * 5));

        if (index > 0)
        {


            index--;
        }

        StomachImage.sprite = StomachSprites[index];
    }
}
