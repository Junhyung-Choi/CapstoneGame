using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergedUIManager : MonoBehaviour
{
    Color gray = new Color(0f,0f,0f,0.72f);
    Color orange = new Color(1,0.5f,0,1);

    Transform canvas;

    private void Start() {
        canvas = this.transform;
    }

    private void Update() {
        float leftValue = GetLeftValue();
        float rightvalue = GetRightValue();
        float middleVau = GetMiddleValue();
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
}
