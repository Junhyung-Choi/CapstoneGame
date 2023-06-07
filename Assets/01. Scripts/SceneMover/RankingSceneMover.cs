using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingSceneMover : SceneMover
{
    public float diff = 20.0f;
    float offTimer, offMaxTime = 0.5f;

    bool isStartReady = false;

    // Update is called once per frame
    void Update()
    {
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
        
        float middleValue = 
            RPInputManager.inputMatrix[0,1] + 
            RPInputManager.inputMatrix[0,2] + 
            RPInputManager.inputMatrix[1,1] + 
            RPInputManager.inputMatrix[1,2];

        if(leftValue < 4f && rightValue < 4f)
        {
            offTimer += Time.unscaledDeltaTime;
            if(offTimer > offMaxTime)
            {
                isStartReady = true;
            }
        }

        if(isStartReady)
        {
            MoveScene(middleValue);
        }
    }

    void MoveScene(float middleValue)
    {
        if(middleValue > diff)
        {
            this.MoveToStart();
        }
    }
}
