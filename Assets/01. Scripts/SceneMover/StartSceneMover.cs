using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneMover : SceneMover
{
    private void Update() {
        float leftValue = 
            RPInputManager.inputMatrix[0,0] + 
            RPInputManager.inputMatrix[0,1] +
            RPInputManager.inputMatrix[1,0] + 
            RPInputManager.inputMatrix[1,1];

        float rightValue = 
            RPInputManager.inputMatrix[0,2] + 
            RPInputManager.inputMatrix[0,3] + 
            RPInputManager.inputMatrix[1,2] + 
            RPInputManager.inputMatrix[1,3];
        
        MoveScene(leftValue, rightValue);
    }

    void MoveScene(float left, float right)
    {
        if(left + 1 < right)
        {
            this.MoveToGame();
        }
        if(right + 1 < left)
        {
            this.MoveToSetting();
        }
    }
}
