using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UnMergedUIManager : MonoBehaviour
{
    
    Color gray = new Color(0f,0f,0f,0.72f);
    Color orange = new Color(1,0.5f,0,1);

    Transform canvas;
    Transform imageParent;
    List<Image> images = new List<Image>();

    private void Start() {
        canvas = this.transform;
        imageParent = this.transform.Find("Steps_8");

        for(int i = 0; i < 8; i++)
            images.Add(imageParent.GetChild(i).GetComponent<Image>());

    }
    
    private void Update() {
        for(int i = 0; i < 4; i++)
        {
            images[i * 2].color = Color.Lerp(gray, orange, Map(RPInputManager.inputMatrix[0,i], 1f, 30f));
            images[i * 2 +1].color = Color.Lerp(gray, orange, Map(RPInputManager.inputMatrix[1,i], 1f, 30f));
        }
    }

    float Map(float value, float min, float max)
    {
        float ret_value = 1f;

        if(value < min) { ret_value = 0f; }
        else if(value < max)
        { 
            ret_value = (value - min) / (max - min);
        }
        return ret_value;
    }
}
