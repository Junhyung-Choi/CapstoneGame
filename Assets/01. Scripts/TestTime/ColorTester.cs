using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorTester : MonoBehaviour
{
    Slider slider;
    Image image;

    Color gray = new Color(0f,0f,0f,0.72f);
    Color orange = new Color(1,0.5f,0,1);

    // Start is called before the first frame update
    void Start()
    {
        slider = transform.Find("Slider").GetComponent<Slider>();
        image = transform.Find("Image").GetComponent<Image>();
    }


    void Update()
    {
        image.color = Color.Lerp(gray, orange, slider.value);
    }   
}
