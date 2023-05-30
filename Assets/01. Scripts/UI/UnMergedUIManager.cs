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
    
    private void Update() {
        canvas = this.transform;
        imageParent = this.transform.Find("Steps_8");

        for(int i = 0; i < 8; i++)
            images.Add(imageParent.GetChild(0).GetComponent<Image>());

    }

    void ShowViewer()
    {
        Transform inputViewer = canvas.GetChild(0);

        for(int i = 0; i < 8; i++)
            images[i].color = Color.Lerp(gray, orange, Map(RPInputManager.inputMatrix[i/4,i%4], 5f, 45f));
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
