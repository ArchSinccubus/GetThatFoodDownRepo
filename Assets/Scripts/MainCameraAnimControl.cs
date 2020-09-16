using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraAnimControl : MonoBehaviour
{
    public GameObject slider;
    public GameObject Seperator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hideSlider()
    {
        slider.SetActive(false);
        Seperator.SetActive(false);

    }

    public void showSlider()
    {
        slider.SetActive(true);
        Seperator.SetActive(true);


    }
}
