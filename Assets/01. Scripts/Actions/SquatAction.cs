using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquatAction : Action
{
    float timer;
    float maxTime = 1.5f;
    bool isSquatStart = false;
    bool isSqautEnd = false;
    float start_threshold = 0f;
    float end_threshold = 0f;

    /// <summary>
    /// 1회의 동작을 검사하는 함수.
    /// base.CheckRep() 에선 동작을 본인의 Set에 보낸다.
    /// </summary>
    public override void CheckRep()
    {
        float backValue = RPInputManager.inputMatrix[1,2] + RPInputManager.inputMatrix[0,3];
        // Debug.Log(backValue);
        // Debug.Log("isStarted : " + isStarted + "/ isSquatStart : " + isSquatStart + "/ isSqautEnd : " + isSqautEnd);
            
        if(isStarted & base.isThresholdSet)
        {
            CheckSquat(backValue);
        }
    }

    public override void SetThreshold(float threshold)
    {
        Debug.Log("SetThreshold");
        base.SetThreshold(threshold);
        this.start_threshold = threshold * 0.2f;
        this.end_threshold = threshold * 0.1f;
    }

    /// <summary>
    /// 동작이 끝난 후 변수들을 초기화 하는 함수.
    /// base.InitRep() 에선 isStarted = false로 바뀌어 추가 세트가 바로 시작되지 않게 한다.
    /// </summary>
    public override void InitRep()
    {
        base.threshold = 100f;
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


    void CheckSquat(float backValue)
    {
        // 아무것도 아닌 상태에서 시작
        if(!isSquatStart & base.isThresholdSet)
        {
            Debug.Log("내려가!");
            // 특정 무게보다 높아진다면 타이머 체크 시작
            if(backValue > threshold + start_threshold)
            {
                isSquatStart = true;
                isSqautEnd = false;
            }
        }
        else
        {
            // 타이머 체크 시작
            // 타이머 완료 안되어 있음.
            if(!isSqautEnd)
            {
                // 타이머 체크 도중 밟는게 너무 가벼워짐 -> 회수 인정 안함.
                if(backValue < end_threshold) { 
                    isSquatStart = false; 
                    isSqautEnd = false; 
                }
                timer += Time.deltaTime;
                if(timer > maxTime)
                {
                    isSqautEnd = true;
                    timer = 0.0f;
                    // base.CheckRep();
                }
            }
            // 타이머 완료 되어있음.
            else
            {
                if(backValue < threshold + end_threshold)
                {
                    base.CheckRep();
                    isSquatStart = false;
                    isSqautEnd = false;
                }
                else
                {
                    Debug.Log("올라와!!");
                }
            }
        }
    }
}
