using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquatAction : Action
{
    public float threshold = 30f;
    public bool isThresholdSet = false;
    float timer;
    float maxTime = 2f;
    bool isSquatStart = false;
    bool isSquatEnd = false;
    float start_threshold = 0f;
    float end_threshold = 0f;

    float stableTimer = 0f, stableMaxTime = 3f;
    bool isUserStable = false;
    float[,] inputMatrix = new float[2,4];
    int inputCount = 0;

    /// <summary>
    /// 1회의 동작을 검사하는 함수.
    /// base.CheckRep() 에선 동작을 본인의 Set에 보낸다.
    /// </summary>
    public override void CheckRep()
    {
        float backValue = RPInputManager.inputMatrix[1,0] + RPInputManager.inputMatrix[1,1] + RPInputManager.inputMatrix[1,2] + RPInputManager.inputMatrix[1,3];
        // Debug.Log(backValue);
        // Debug.Log("isStarted : " + isStarted + "/ isThresholdSet : " + isThresholdSet);
            
        if(base.isStarted & isThresholdSet)
        {
            CheckSquat(backValue);
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
        RPInputManager.instance.ShowNotice("스쿼트를 시작하기 위해선\n 안정화 작업이 필요합니다." + "\n\n" + "스텝박스 위에 올라가 안정되게 서 주세요.");
        Debug.Log("스쿼트를 시작하기 위해선 안정화 작업이 필요합니다.");
        Debug.Log("스텝박스 위에 올라가 안정되게 서 주세요.");
    }

    public override void _TestSquat(float t)
    {
        Debug.LogWarning("태스트용 threshold 변경");
        SetThreshold(t);
    }

    public void SetThreshold(float threshold) 
    {
        if(PlayerPrefs.GetInt("UserWeight") > 60)
        {
            this.start_threshold = threshold * 0.2f;
            this.end_threshold = threshold * 0.2f;
        }
        else
        {
            this.start_threshold = threshold * 0.12f;
            this.end_threshold = threshold * 0.12f;
        }

        isThresholdSet = true;
        this.threshold = threshold;
        
        Debug.Log("threshold : " + threshold + "/ start_threshold : " + start_threshold + "/ end_threshold : " + end_threshold);
    }

    void _InitRep()
    {
        isSquatStart = false;
        isSquatEnd = false;
        timer = 0f;
        
        float sum = ActionManager.avgInputMatrix[1,0] + ActionManager.avgInputMatrix[1,1] + ActionManager.avgInputMatrix[1,2] + ActionManager.avgInputMatrix[1,3];
        SetThreshold(sum);
    }

    void CheckSquat(float backValue)
    {
        if(!isUserStable)
        {
            MakeUserStable();
            return ;
        }

        // 아무것도 아닌 상태에서 시작
        if(!isSquatStart & isThresholdSet)
        {
            bool isBackValueHigher = backValue > threshold + start_threshold;
            // 특정 무게보다 높아진다면 타이머 체크 시작
            if(isBackValueHigher)
            {
                isSquatStart = true;
                isSquatEnd = false;
            }
        }
        else
        {
            // 타이머 체크 시작
            // 타이머 완료 안되어 있음.
            if(!isSquatEnd)
            {
                // 타이머 체크 도중 밟는게 너무 가벼워짐 -> 회수 인정 안함.
                if(backValue < threshold + end_threshold) { 
                    isSquatStart = false; 
                    isSquatEnd = false; 
                }
                else{
                    timer += Time.unscaledDeltaTime;
                    progress = this.Map(timer, 0f, maxTime);
                    if(timer > maxTime)
                    {
                        isSquatEnd = true;
                        timer = 0.0f;
                        // base.CheckRep();
                    }
                }
            }
            // 타이머 완료 되어있음.
            else
            {
                if(backValue < threshold + end_threshold)
                {
                    RPInputManager.instance.ShowNotice("Perfect!");
                    Debug.Log("한 회 완료");
                    base.CheckRep();
                    isSquatStart = false;
                    isSquatEnd = false;
                }
                else
                {
                    RPInputManager.instance.ShowNotice("올라오세요!");
                    Debug.Log("올라오세요!");
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
                    diff += ActionManager.avgInputMatrix[i,j] - (inputMatrix[i,j] / inputCount);
                    inputMatrix[i,j] = 0f;
                }
            }
            inputCount = 0;

            if(diff < 10)
            {
                isUserStable = true;
                RPInputManager.instance.ShowNotice("사용자 안정화 완료\n\n 스쿼트를 시작해주세요.");
                Debug.Log("사용자 안정화 완료\n 스쿼트를 시작해주세요.");
            }
            else
            {
                RPInputManager.instance.ShowNotice("사용자 안정화 작업중입니다\n 안정된 자세로 잠시 기다려주세요.");
                Debug.Log("사용자 안정화 실패");
            }
        }
    }
}
