using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialChecker : SceneMover
{
    public GameObject tutorialUI;
    Slider slider;
    Text text;

    float rightStepTimer = 0f;
    float rightStepMaxTime = 5f;
    float rightStepThreshold = 0f;
    float rightStepThresholdRate = 0.8f;

    int tutoCount = 0;
    int maxTutoCount = 5; // Walk, StepUp, Plank, Sqaut, ArmWalk

    private void Start() {
        slider = tutorialUI.transform.Find("Slider").GetComponent<Slider>();
        text = tutorialUI.transform.Find("Instruction").GetComponent<Text>();

        slider.maxValue = rightStepMaxTime;

        SetRightStepThreshold();
    }
    
    private void Update() {
        CheckTutorialEnd();
        
    }

    void CheckTutorialEnd()
    {
        if(CheckRightStep()) // Check Right Step
        {
            rightStepTimer += Time.deltaTime;
            slider.value = rightStepTimer;

            if(rightStepTimer > rightStepMaxTime) { this.MoveToGame(); }
        }
        else 
        { 
            rightStepTimer = 0; 
            slider.value = 0f;
        }
    }

    bool CheckRightStep()
    {
        float sum = 0f;
        
        sum = RPInputManager.inputMatrix[1,2] + 
              RPInputManager.inputMatrix[0,2] + 
              RPInputManager.inputMatrix[1,3] + 
              RPInputManager.inputMatrix[0,3];

        if(sum < rightStepThreshold) { return false; }

        return true;
    }


    void SetRightStepThreshold()
    {
        float sum = 0f;

        for(int i = 0; i < ActionManager.avgInputMatrix.GetLength(0); i++)
        {
            for(int j = 0; j < ActionManager.avgInputMatrix.GetLength(1); j++)
            {
                sum += ActionManager.avgInputMatrix[i,j];
            }
        }

        // rightStepThreshold = sum * rightStepThresholdRate;
        rightStepThreshold = 0.5f;
    }
}
