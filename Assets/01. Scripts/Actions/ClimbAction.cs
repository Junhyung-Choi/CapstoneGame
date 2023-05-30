using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClimbAction : Action
{
    public float threshold = 30f;   // 암워크 동작의 기준이 되는 값
    public bool isThresholdSet = false; // 암워크 동작의 기준이 되는 값이 설정되었는지 확인하는 변수

    /// <summary>
    /// 암워크 동작 무효화를 위한 변수들
    /// </summary>
    float resetTimer = 0.0f, resetMaxTime = 1.5f;

    bool isUpped = false;
    bool isSideRight = false, isSideChecked = false; 

    /// <summary>
    /// 각 지점별 영역 값을 보기 위한 변수들
    /// </summary>
    float startPointValue, endPointValue, totalValue;

    float stableTimer, stableMaxTime = 1f; // 플랭크 동작이 시작되기 전 안정화 시간
    float[,] inputMatrix = new float[2,4];
    int inputCount = 0;
    bool isUserStable = false;

    /// <summary>
    /// 1회의 동작을 검사하는 함수.
    /// base.CheckRep() 에선 동작을 본인의 Set에 보낸다.
    /// </summary>
    public override void CheckRep()
    {

        // Debug.Log("ClimbAction CheckRep");
        if(isStarted & isThresholdSet)
        {
            CheckClimb();
        }
    }

    /// <summary>
    /// 동작이 끝난 후 변수들을 초기화 하는 함수.
    /// base.InitRep() 에선 isStarted = false로 바뀌어 추가 세트가 바로 시작되지 않게 한다.
    /// </summary>
    public override void InitRep()
    {
        _InitRep();
        base.InitRep();
    }

    /// <summary>
    /// 동작이 시작될때 isStarted를 true로 바꾸는 함수.
    /// base.StartRep() 에선 isStarted = true로 바꾸어 세트를 시작할 수 있도록 만든다.
    /// </summary>
    public override void StartRep()
    {
        base.StartRep();
    }

    public override void _TestSquat(float t)
    {
        RPInputManager.instance.ShowNotice("태스트용 threshold 변경");
        Debug.LogWarning("태스트용 threshold 변경");
        SetThreshold(t);
        isThresholdSet = true;
    }

    public void SetThreshold(float threshold)
    {
        isThresholdSet = true;
        this.threshold = threshold;
        // this.start_threshold = threshold * 0.2f;
        // this.end_threshold = threshold * 0.1f;
    }

    void _InitRep()
    {
        isUpped = false;
        resetTimer = 0f;
        isSideChecked = false;

        float sum = 0f;
        for(int i = 0; i < 4; i++)
        {
            sum += ActionManager.avgInputMatrix[0,i];
            sum += ActionManager.avgInputMatrix[1,i];      
        }
        sum /= 8f;
        SetThreshold(sum * 0.5f);
    }

    void SetIsSideRight()
    {
        bool isRight = RPInputManager.inputMatrix[0,3] + 
                       RPInputManager.inputMatrix[1,3] 
                       > 
                       RPInputManager.inputMatrix[1,0] + 
                       RPInputManager.inputMatrix[0,0] + threshold;

        bool isLeft  = RPInputManager.inputMatrix[0,0] + 
                       RPInputManager.inputMatrix[1,0]
                       >
                       RPInputManager.inputMatrix[0,3] +
                       RPInputManager.inputMatrix[1,3] + threshold;
        
        if (isRight) { isSideRight = true; }   // 오른쪽
        if (isLeft)  { isSideRight =  false; }   // 왼쪽

        if(isRight || isLeft)
        {
            isSideChecked = true;
            resetTimer = 0f;
        }

        // Debug.Log("isSideRight : " + isSideRight + " / isSideChecked : " + isSideChecked);
    }

    void SetPointValue()
    {
        if(isSideRight)
        {
            startPointValue = RPInputManager.inputMatrix[0,3] + RPInputManager.inputMatrix[1,3];
            endPointValue = RPInputManager.inputMatrix[1,0] + RPInputManager.inputMatrix[0,0];
        }
        else
        {
            startPointValue = RPInputManager.inputMatrix[0,0] + RPInputManager.inputMatrix[1,0];
            endPointValue = RPInputManager.inputMatrix[1,3] + RPInputManager.inputMatrix[0,3];
        }

        for(int i = 0; i < 4; i++)
        {
            totalValue += RPInputManager.inputMatrix[0,i];
            totalValue += RPInputManager.inputMatrix[1,i];
        }
    }

    void CheckUndo()
    {
        if(totalValue < threshold)
        {
            resetTimer += Time.unscaledDeltaTime;
            if(resetTimer > resetMaxTime)
            {
                isUpped = false;
                resetTimer = 0f;
            }
        }
        else
        {
            resetTimer = 0f;
        }
    }

    void CheckClimb()
    {
        if(!isUserStable) { MakeUserStable(); return; }

        if(!isSideChecked) { SetIsSideRight(); }
        else
        {

            // Debug.Log("isUpped : " + isUpped);
            SetPointValue();

            if(!isUpped)
            {
                CheckUndo();

                if(endPointValue > threshold) { isUpped = true; }
            }
            else
            {
                if(startPointValue > threshold)
                {
                    isUpped = false;
                    base.CheckRep();
                }
            }

        }
    }

    void MakeUserStable()
    {
        stableTimer += Time.unscaledDeltaTime;
        for(int i = 0; i < 2; i++)
        {
            for(int j = 0; j < 4; j++)
            {
                inputMatrix[i,j] += RPInputManager.inputMatrix[i,j];
            }
        }
        inputCount += 1;
        
        if(stableTimer > stableMaxTime)
        {
            stableTimer = 0f;

            float diff = 0f;
            for(int i = 0; i < 2; i++)
            {
                for(int j = 0; j < 4; j++)
                {
                    diff += inputMatrix[i,j] / inputCount;
                    inputMatrix[i,j] = 0f;
                }
            }
            inputCount = 0;

            if(diff < 5)
            {
                isUserStable = true;
                RPInputManager.instance.ShowNotice("올라가기 운동 시작 가능");
                Debug.Log("올라가기 운동 시작 가능");
            }
            else
            {
                RPInputManager.instance.ShowNotice("내려오세용.");
                Debug.Log("내려오세용.");
            }
        }
    }
}