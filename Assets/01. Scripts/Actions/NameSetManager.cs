using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSetManager
{
    //이름 설정 메트릭스 상의 좌표
    int x = 0;
    int y = 0;
    int count = 0;

    float Timer = 0f;
    float MaxTimer = 1f;

    bool isSideRight = false; // false = 왼쪽, true = 오른쪽

    bool isSideCheckStart = false; // false = 체크 시작 안함, true = 체크 시작함
    bool isSideCheckEnd = false;

    float threshold = 0f; // 기준이 되는 값 

    void Start()
    {
        initVariables();
        SetThreshold();
    }

    void initVariables()
    {
        isSideRight = false;
        isSideCheckStart = false;
        isSideCheckEnd = false;
        count = 0,x = 0,y = 0;
        Timer = 0f;
    }

    void SetThreshold()
    {
        float sum = 0f;
        for(int i = 0; i < 4; i++)
        {
            sum += ActionManager.avgInputMatrix[0,i];
            sum += ActionManager.avgInputMatrix[1,i];
        }
        sum /= 8f;
        threshold = sum * 0.5f;
    }

    void Update()
    {
        // 아직 사이드 체크 안 됨
        if(!isSideCheckEnd)
        {
            GetStep();
        }
        else    // 사이드 체크 됨
        {
            isSideCheckEnd = false;
            SetMatrix();
        }
    }

    void SetMatrix()
    {
        if(isSideRight)
        {
            count++;
            if(count % 7 != 0)  { x++; }
            else { y++; x = 0; }
        }
        else
        {
            count--;
            if(count % 7 != 0) { x--; }
            else { y--; x = 5; }
        }

    }

    void GetStep()
    {

        float rightValue = RPInputManager.inputMatrix[0,3] + RPInputManager.inputMatrix[1,3];
        float leftValue = RPInputManager.inputMatrix[0,0] + RPInputManager.inputMatrix[1,0];

        if(isSideCheckStart)
        {
            Timer += Time.deltaTime;
            if(isSideRight)
            {
                if(rightValue < threshold || Timer > MaxTimer) { 
                    isSideCheckEnd = true;
                    isSideCheckStart = false;
                    Timer = 0f;
                    return;
                }
            }
            else
            {
                if(leftValue < threshold || Timer > MaxTimer) { 
                    isSideCheckEnd = true;
                    isSideCheckStart = false;
                    Timer = 0f;
                    return;
                }
            }
        }
        else{
            if(rightValue > leftValue + threshold)
            {
                isSideCheckStart = true;
                isSideRight = true;
            }
            else if(leftValue > rightValue + threshold)
            {
                isSideCheckStart = true;
                isSideRight = false;
            }
        }
    }
}
