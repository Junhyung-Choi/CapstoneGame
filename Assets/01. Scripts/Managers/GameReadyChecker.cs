using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameReadyChecker : MonoBehaviour
{
    StartSetSceneMover startSetSceneMover;
    public bool isFirstAverageChecked = false;
    public bool isGameReady = false;
    
    int count = 0;
    int avgCount = 0;
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
            GainData();
            count += 1;
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
    }

    void ResetAvg()
    {
        Debug.Log("자세가 불안정합니다. 측정을 재시작합니다.");
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

}
