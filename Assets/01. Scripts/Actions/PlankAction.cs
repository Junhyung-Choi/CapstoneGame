using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlankAction : Action
{
    public float threshold = 30f;   // 플랭크 동작의 기준이 되는 값
    public bool isThresholdSet = false; // 플랭크 동작의 기준이 되는 값이 설정되었는지 확인하는 변수
    float timer = 0.0f, maxTime = 2f;   // 플랭크 동작의 최대 시간
    float resetTimer = 0.0f, resetMaxTime = 1f;   // 플랭크 동작 리셋 시간

    float stableTimer, stableMaxTime = 1f; // 플랭크 동작이 시작되기 전 안정화 시간
    float[,] inputMatrix = new float[2,4];
    int inputCount = 0;
    bool isUserStable = false;



    bool isPlankStart = false, isJumped = false;

    /// <summary>
    /// 1회의 동작을 검사하는 함수.
    /// base.CheckRep() 에선 동작을 본인의 Set에 보낸다.
    /// </summary>
    public override void CheckRep()
    {
        float inputValue = RPInputManager.inputMatrix[1,1] + RPInputManager.inputMatrix[1,2];
        
        if(isStarted & isThresholdSet)
        {
            CheckPlank(inputValue);
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
        RPInputManager.instance.ShowNotice("플랭크를 시작하기 위해선\n 안정화 작업이 필요합니다.\n\n 스텝박스에서 내려오세요.");
        Debug.Log("플랭크를 시작하기 위해선 안정화 작업이 필요합니다.");
        Debug.Log("스텝박스에서 내려오세요.");
    }

    public override void _TestSquat(float t)
    {
        // RPInputManager.instance.ShowNotice("테스트용 threshold 변경");
        Debug.LogWarning("테스트용 threshold 변경");
        SetThreshold(t);
        isThresholdSet = true;
    }

    public void SetThreshold(float threshold)
    {
        isThresholdSet = true;
        this.threshold = threshold * 0.2f;
    }

    void _InitRep()
    {
        isPlankStart = false;
        isJumped = true;
        timer = 0.0f;
        isThresholdSet = false;
        timer = 0.0f; maxTime = 2f;
        resetTimer = 0.0f; resetMaxTime = 1f;

        float sum = ActionManager.avgInputMatrix[1,1] + ActionManager.avgInputMatrix[1,2];
        SetThreshold(sum);
    }

    void CheckStart(float inputValue)
    {
        if(inputValue > threshold)
        {
            isPlankStart = true;
            timer = 0.0f;
        }
    }

    void CheckUndo()
    {
        resetTimer += Time.unscaledDeltaTime;
        if(resetTimer > resetMaxTime)
        {
            isPlankStart = false;
            resetTimer = 0.0f;
            progress = 0f;
        }
    }

    void CheckPlank(float inputValue)
    {
        if(!isUserStable) { MakeUserStable(); return ;}

        if(!isPlankStart) { CheckStart(inputValue); }
        else
        {
            if(inputValue > threshold)
            {
                if(!isJumped) { 
                    RPInputManager.instance.ShowNotice("일어나세요.");
                    Debug.Log("올라오세요"); return ; 
                }
                timer += Time.unscaledDeltaTime;
                progress = this.Map(timer, 0f, maxTime);
                if(timer > maxTime)
                {
                    isJumped = false;
                    // isPlankStart = false;
                    timer = 0.0f;
                    AudioManager.instance.PlayEndRepEffect();
                }
            }
            else
            {
                if(!isJumped)
                {
                    isJumped = true;
                    isPlankStart = false;
                    timer = 0.0f;
                    progress = 0f;
                    this.DoRep();
                }
                else
                {
                    CheckUndo();
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
                RPInputManager.instance.ShowNotice("플랭크 운동을 시작해주세요.");
                Debug.Log("플랭크 운동입니다");
            }
            else
            {
                // RPInputManager.instance.ShowNotice("내려오세요.");
                Debug.Log("내려오세요!");
            }
        }
    }
}