using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameReadyChecker : MonoBehaviour
{
    StartSetSceneMover startSetSceneMover;
    public GameObject instruction;
    Text instructionText;

    bool isFirstAverageChecked = false;
    bool isGameReady = false;
    bool isFirstStepStarted = false;
    
    int count = 0;
    int avgCount = 0;

    float stepThreshold = 0.5f;

    float avgTimer = 0.0f;
    float avgMaxTime = 2f;

    float maxDiff = 30f;
    float diff = 0f;

    float[,] avgMatrix = new float[2,4];
    float[,] inputMatrix = new float[2,4];
    // Start is called before the first frame update
    void Start()
    {
        startSetSceneMover = this.transform.GetComponent<StartSetSceneMover>();
        instructionText = instruction.transform.Find("Instruction").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isGameReady)
        {
            GetInputMatrix();
        }
        else
        {
            ActionManager.avgInputMatrix = avgMatrix;
            startSetSceneMover.MoveScene();
        }
    }

    void GetInputMatrix()
    {
        avgTimer += Time.deltaTime;

        if(avgTimer > avgMaxTime)
        {
            avgTimer = 0.0f;
            if(isFirstAverageChecked)
            {
                CheckDiff();

                if(diff < maxDiff) { RenewAvg(); }
                else { ResetAvg(); }

                if(avgCount >= 5) { isGameReady = true; }
            }
            else
            {
                SetAvg();

                avgCount = 1;
                isFirstAverageChecked = true;
            }

            count = 0;
            diff = 0;
        }
        else
        {
            if(!isFirstStepStarted) { CheckFirstStep(); }
            else
            {
                GainData();
                count += 1;
            }
        }
    }

    void CheckDiff()
    {
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                diff += Mathf.Abs(avgMatrix[i,j] - inputMatrix[i,j] / count);
            }
        }
    }

    void RenewAvg()
    {
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                avgMatrix[i,j] =  (avgMatrix[i,j] + (inputMatrix[i,j] / count)) / 2;
            }
        }
        avgCount += 1;
        Debug.Log("자세 측정중입니다. " + avgCount + " / 5번째 측정중입니다.");
        instructionText.text = "자세 측정중입니다. " + avgCount + " / 5번째 측정중입니다.";
    }

    void ResetAvg()
    {
        Debug.Log("자세가 불안정합니다. 측정을 재시작합니다.");
        instructionText.text = ("자세가 불안정합니다. 측정을 재시작합니다.");
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                avgMatrix[i,j] = inputMatrix[i,j] / count;
                avgCount = 0;
            }
        }
    }

    void SetAvg()
    {
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                avgMatrix[i,j] = inputMatrix[i,j] / count;
            }
        }
    }

    void GainData()
    {
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                inputMatrix[i,j] += RPInputManager.inputMatrix[i,j];
            }
        }
    }

    void CheckFirstStep()
    {
        float sum = 0.0f;
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                sum += RPInputManager.inputMatrix[i,j];
            }
        }

        if(sum > stepThreshold)
        {
            isFirstStepStarted = true;
            instructionText.text = "자세 측정중입니다. 1 / 5번째 측정중입니다.";
        }
    }

}
