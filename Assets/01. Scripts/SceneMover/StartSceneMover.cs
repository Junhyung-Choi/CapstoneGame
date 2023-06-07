using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneMover : SceneMover
{
    public float diff = 20.0f;
    float offTimer, offMaxTime = 0.5f;

    bool isStartReady = false;

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

        if(leftValue < 2f && rightValue < 2f)
        {
            offTimer += Time.unscaledDeltaTime;
            if(offTimer > offMaxTime)
            {
                isStartReady = true;
            }
        }

        if(isStartReady)
        {
            MoveScene(leftValue, rightValue);
        }
        
    }

    void MoveScene(float left, float right)
    {
        if(left + diff < right)
        {
            AudioManager.instance.PlayClickButtonEffect();
            this.MoveToGameStart();
        }
        if(right + diff < left)
        {
            AudioManager.instance.PlayClickButtonEffect();
            this.MoveToSetting();
        }
    }
}
