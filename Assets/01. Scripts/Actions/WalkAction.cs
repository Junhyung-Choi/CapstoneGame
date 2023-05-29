using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkAction : Action
{

    public bool isFirstStep = false, isStepEnd = true;
    bool isFirstStepLeft = false;
    float timer = 0.0f, maxTime = 10f;
    float threshold = 30f;


    float leftValue, rightValue;

    /// <summary>
    /// 1회의 동작을 검사하는 함수.
    /// base.CheckRep() 에선 동작을 본인의 Set에 보낸다.
    /// </summary>
    public override void CheckRep()
    {
        _CheckRep();
    }

    void _CheckRep()
    {
        timer += Time.deltaTime;
        leftValue = 
            (RPInputManager.inputMatrix[1,0] + RPInputManager.inputMatrix[0,0] +
            RPInputManager.inputMatrix[0,1] + RPInputManager.inputMatrix[1,1]) / 4;

        rightValue = 
            RPInputManager.inputMatrix[1,2] + RPInputManager.inputMatrix[0,2] +
            RPInputManager.inputMatrix[0,3] + RPInputManager.inputMatrix[1,3] / 4;
        
        if(timer > maxTime) { 
            timer = 0f;
            return; 
        }
        else{
            if(!isStepEnd) { CheckStepEnd(); return;}

            if(!isFirstStep){
                CheckFirstStep(rightValue,leftValue);
            }
            else{
                CheckNextStep(rightValue,leftValue);
            }
        }
    }

    void CheckFirstStep(float rightValue,float leftValue){
        // if(leftValue > threshold * 0.5f)
        if(leftValue > threshold * 0.3f)
        {
            isFirstStep = true;
            isFirstStepLeft = true;
            timer = 0.0f;
        }
        // if(rightValue > threshold * 0.5f)
        if(rightValue > threshold * 0.3f)
        {
            isFirstStep = true;
            isFirstStepLeft = false;
            timer = 0.0f;
        }
    }

    void CheckNextStep(float rightValue,float leftValue){
        if(isFirstStepLeft)
        {
            if(rightValue > threshold * 0.3f) { isStepEnd = false; }
        }
        else
        {
            if(leftValue > threshold * 0.3f) { isStepEnd = false; }
        }
    }

    void CheckStepEnd()
    {
        if(leftValue < 1 && rightValue < 1)
        {
            isStepEnd = true;
            isFirstStep = false;
            this.DoRep();
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

    public override void _TestSquat(float t)
    {
        Debug.LogWarning("태스트용 threshold 변경");
        SetThreshold(t);
        isStarted = true;
    }

    public void _InitRep()
    {
        this.isFirstStep = false;
        this.isFirstStepLeft = false;
        this.timer = 0.0f;
        
        this.threshold = 0f;
        for(int i = 0; i < 4 ; i++){
            this.threshold += ActionManager.avgInputMatrix[0,i];
            this.threshold += ActionManager.avgInputMatrix[1,i];
        }
        this.threshold = this.threshold / 2;
        this.threshold = this.threshold * 0.5f;
        Debug.Log("threshold : " + threshold);
        // SetThreshold(this.threshold);
    }

    public void SetThreshold(float threshold)
    {
        this.threshold = threshold;
        // Debug.Log("threshold : " + threshold);
    }

    /// <summary>
    /// 동작이 시작될때 isStarted를 true로 바꾸는 함수.
    /// base.StartRep() 에선 isStarted = true로 바꾸어 세트를 시작할 수 있도록 만든다.
    /// </summary>
    public override void StartRep()
    {
        _StartRep();
        base.StartRep();
    }

    void _StartRep()
    {
    //     Debug.Log("플랭크를 시작하기 위해선 안정화 작업이 필요합니다.");
    //     Debug.Log("스텝박스에서 내려오세용.");
    }

}