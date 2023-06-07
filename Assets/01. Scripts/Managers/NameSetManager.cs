using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameSetManager : MonoBehaviour
{
    public bool isDemo = false;

    //이름 설정 메트릭스 상의 좌표
    int x = 0, y = 0;
    int count = 0;

    float Timer = 0f, MaxTimer = 1.2f;

    bool isSideRight = false; // false = 왼쪽, true = 오른쪽

    bool isStepStarted = false;
    bool isConfirmClicked = false;
    bool isConfirmStarted = false;
    bool isKeepPressing = false;

    float threshold = 0f; // 기준이 되는 값 

    RankingSceneManager rankingSceneManager;

    void Start()
    {
        initVariables();
        SetThreshold();
    }

    void initVariables()
    {
        isSideRight = false;
        isStepStarted = false;
        isConfirmClicked = false;
        count = 0; x = 0; y = 0;
        Timer = 0f;

        rankingSceneManager = this.transform.GetComponent<RankingSceneManager>();
    }

    void SetThreshold()
    {
        if(isDemo) { threshold = 20f; return; }

        float sum = 0f;
        for(int i = 0; i < 4; i++)
        {
            sum += ActionManager.avgInputMatrix[0,i];
            sum += ActionManager.avgInputMatrix[1,i];
        }
        sum /= 8f;
        threshold = sum * 0.3f;
    }

    void Update()
    {
        // 아직 확인버튼 안눌림.
        if(!isConfirmClicked)
        {
            GetStep();
        }
        else    // 확인버튼 클릭 시
        {
            isConfirmClicked = false; // 초기화 
            if(count == 27){ rankingSceneManager.CompleteName(); } //등록
            else if(count == 26)  { rankingSceneManager.DeleteCharacter(); }//뒤로 가기 
            else { rankingSceneManager.AddCharacter(count); }//count에 따라 알파벳 선정 
        }
    }

    void SetMatrix()
    {
        if(isSideRight)
        {
            count++;
            if(count == 28){
                x = 0; y = 0; count = 0;
            }
            else if(count % 6 != 0)  { x++; }
            else { y++; x = 0; }
        }
        else
        {
            count--;
            if(count == -1){
                x = 3; y = 4; count = 27;
            }
            else if(count % 6 != 5) { x--; }
            else { y--; x = 5; }
        }
        rankingSceneManager.ChangeCursor(count);
    }

    void GetStep()
    {
        float rightValue = RPInputManager.inputMatrix[0,3] + RPInputManager.inputMatrix[1,3];
        float leftValue = RPInputManager.inputMatrix[0,0] + RPInputManager.inputMatrix[1,0];
        float midValue = (RPInputManager.inputMatrix[0,1] + RPInputManager.inputMatrix[1,1] + RPInputManager.inputMatrix[0,2] + RPInputManager.inputMatrix[1,2])/2;
        if(isStepStarted)
        {
            Timer += Time.unscaledDeltaTime;
            if(isSideRight)
            {
                if(rightValue < threshold){
                    if(!isKeepPressing) { //발을 땜
                        isStepStarted = false;
                        Timer = 0f;
                        return;
                    }
                    else{
                        isKeepPressing = false;
                        isStepStarted = false;
                        Timer = 0f;
                        SetMatrix();
                        return;
                    }
                if(Timer > MaxTimer){
                    isKeepPressing = true;
                    Timer = 0f;
                    SetMatrix();
                }
            }
            else
            {
                if(leftValue < threshold) { 
                    if(!isKeepPressing) { //발을 땜
                        isStepStarted = false;
                        Timer = 0f;
                        return;
                    }
                    else{
                        isKeepPressing = false;
                        isStepStarted = false;
                        Timer = 0f;
                        SetMatrix();
                        return;
                    }
                }
                if(Timer > MaxTimer){
                    isKeepPressing = true;
                    Timer = 0f;
                    SetMatrix();
                }
            }
        }
        else{
            if(isConfirmStarted){
                if(midValue < threshold * 0.25) { 
                   isConfirmClicked = true; 
                   isConfirmStarted = false;
                   return;
                }
            }

            if(rightValue > leftValue + threshold && rightValue > midValue + threshold )
            {
                isStepStarted = true;
                isSideRight = true;
            }
            else if(leftValue > rightValue + threshold && leftValue > midValue + threshold)
            {
                isStepStarted = true;
                isSideRight = false;
            }
            else if((midValue > rightValue + threshold || midValue > leftValue + threshold) && !isConfirmStarted)
            {
                isConfirmStarted = true;
            }
        }
    }
}
