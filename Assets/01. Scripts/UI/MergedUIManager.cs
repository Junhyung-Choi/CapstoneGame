using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergedUIManager : MonoBehaviour
{
    Color gray = new Color(0f,0f,0f,1f);
    Color orange = new Color(1,0.5f,0,1);

    Transform canvas;
    Image leftImage, rightImage, middleImage;

    private void Start() {
        canvas = this.transform;

        leftImage = canvas.Find("Steps_3").Find("StepsLeft").GetComponent<Image>();
        rightImage = canvas.Find("Steps_3").Find("StepsRight").GetComponent<Image>();
        middleImage = canvas.Find("Steps_3").Find("StepsMid").GetComponent<Image>();
    }

    private void Update() {
        float leftValue = GetLeftValue();
        float rightvalue = GetRightValue();
        float middleValue = GetMiddleValue();

        leftValue = GetTimeScale(leftValue, 5f, 45f);
        rightvalue = GetTimeScale(rightvalue, 5f, 45f);
        middleValue = GetTimeScale(middleValue, 5f, 45f);

        leftImage.color = Color.Lerp(gray, orange, leftValue);
        rightImage.color = Color.Lerp(gray, orange, rightvalue);
        middleImage.color = Color.Lerp(gray, orange, middleValue);
    }

    float GetLeftValue() {
        float value;
        value = 
            RPInputManager.inputMatrix[0,0] + 
            RPInputManager.inputMatrix[1,0];
        return value;
    }
    float GetRightValue() {
        float value;
        value = 
            RPInputManager.inputMatrix[0,3] + 
            RPInputManager.inputMatrix[1,3];
        return value;
    }

    float GetMiddleValue() {
        float value;
        value = 
            RPInputManager.inputMatrix[0,1] + 
            RPInputManager.inputMatrix[0,2] + 
            RPInputManager.inputMatrix[1,1] + 
            RPInputManager.inputMatrix[1,2];
        return value;
    }

    float GetTimeScale(float value, float min, float max)
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
