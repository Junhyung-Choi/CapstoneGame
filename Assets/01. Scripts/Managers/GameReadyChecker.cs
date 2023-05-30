using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameReadyChecker : MonoBehaviour
{
    StartSetSceneMover startSetSceneMover;
    public GameObject instruction;
    TMPro.TMP_Text instructionText;

    bool isFirstAverageChecked = false;
    bool isGameReady = false;
    bool isFirstStepStarted = false;
    bool isBoxCleared = false;

    int count = 0;
    int avgCount = 0;

    float stepThreshold = 10f;

    float avgTimer = 0.0f;
    float avgMaxTime = 2f;

    float maxDiff = 30f;
    float diff = 0f;

    float[,] avgMatrix = new float[2, 4];
    float[,] inputMatrix = new float[2, 4];

    float boxClearTimer = 0.0f , boxMaxClearTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        startSetSceneMover = this.transform.GetComponent<StartSetSceneMover>();
        instructionText = instruction.transform.Find("GuideBox").Find("Instruction").GetComponent<TMPro.TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isGameReady)
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
        if(!isBoxCleared) { MakeUserClearBox(); return ; }

        if (avgTimer > avgMaxTime)
        {
            avgTimer = 0.0f;
            if (isFirstAverageChecked)
            {
                CheckDiff();

                if (diff < maxDiff) { RenewAvg(); }
                else { ResetAvg(); }

                if (avgCount >= 5) { isGameReady = true; }
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
            if (!isFirstStepStarted) { CheckFirstStep(); }
            else
            {
                avgTimer += Time.deltaTime;
                GainData();
                count += 1;
            }
        }
    }

    void CheckDiff()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                diff += Mathf.Abs(avgMatrix[i, j] - (inputMatrix[i, j] / count));
            }
        }
    }

    void RenewAvg()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                avgMatrix[i, j] = (avgMatrix[i, j] + (inputMatrix[i, j] / count)) / 2;
            }
        }

        inputMatrix = new float[2, 4];
        avgCount += 1;
        // Debug.Log("자세 측정중입니다. " + avgCount + " / 5번째 측정중입니다.");
        instructionText.text = "자세 측정중입니다. " + avgCount + " / 5번째 측정중입니다.";
    }

    void ResetAvg()
    {
        instructionText.text = ("자세가 불안정합니다. 측정을 재시작합니다.");
        inputMatrix = new float[2, 4];
        isFirstAverageChecked = false;
        avgCount = 0;
    }

    void SetAvg()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                avgMatrix[i, j] = inputMatrix[i, j] / count;
            }
        }
        inputMatrix = new float[2, 4];
        count = 0;
    }

    void GainData()
    {
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                inputMatrix[i, j] += RPInputManager.inputMatrix[i, j];
            }
        }
    }

    void CheckFirstStep()
    {
        float sum = 0.0f;
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                sum += RPInputManager.inputMatrix[i, j];
            }
        }

        if (sum > stepThreshold)
        {
            isFirstStepStarted = true;
            instructionText.text = "자세 측정중입니다. 1 / 5번째 측정중입니다.";
        }
    }

    void MakeUserClearBox()
    {
        Debug.Log("박스를 비워주세요");
        instructionText.text = "박스를 비워주세요";

        if (boxClearTimer > boxMaxClearTime)
        {
            boxClearTimer = 0.0f;
            float sum = 0;
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    sum += (inputMatrix[i, j] / count);
                    inputMatrix[i, j] = 0;
                }
            }
            count = 0;
            if(sum < 10f)
            {
                isBoxCleared = true;
                instructionText.text = "자세 측정할거니까 올라오세요";
            }
            else
            {
                Debug.Log("박스를 비우라구요");
                instructionText.text = "박스를 비우라구요";
            }
        }
        else
        {
            boxClearTimer += Time.deltaTime;
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    inputMatrix[i, j] += RPInputManager.inputMatrix[i, j];
                }
            }
            count += 1;
        }
    }

}
