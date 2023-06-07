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

    enum Direction { LEFT, RIGHT, MIDDLE, NONE};
    Direction direction = Direction.NONE;

    bool isStepStarted = false;
    bool isConfirmClicked = false;
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
        direction = Direction.NONE;
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
            AudioManager.instance.PlayClickButtonEffect();
            isConfirmClicked = false; // 초기화 
            if(count == 27){ rankingSceneManager.CompleteName(); } //등록
            else if(count == 26)  { rankingSceneManager.DeleteCharacter(); }//뒤로 가기 
            else { rankingSceneManager.AddCharacter(count); }//count에 따라 알파벳 선정 
        }
    }

    void SetMatrix()
    {
        if(direction == Direction.RIGHT)
        {
            count++;
            if(count == 28){
                x = 0; y = 0; count = 0;
            }
            else if(count % 6 != 0)  { x++; }
            else { y++; x = 0; }
        }
        else if (direction == Direction.LEFT)
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
            WaitForRealInput(leftValue, midValue, rightValue);
        }
        else
        {
            WaitForInitStep(leftValue, midValue, rightValue);
        }
    }

    void WaitForInitStep(float leftValue, float midValue, float rightValue)
    {
        if(leftValue > threshold && rightValue < threshold * 0.5f && midValue < threshold * 0.8f)
        {
            direction = Direction.LEFT;
            isStepStarted = true;
            return;
        }

        if(rightValue > threshold && leftValue < threshold * 0.5f && midValue < threshold * 0.8f)
        {
            direction = Direction.RIGHT;
            isStepStarted = true;
            return;
        }

        if(midValue > threshold && leftValue < threshold * 0.5f && rightValue < threshold * 0.5f)
        {
            direction = Direction.MIDDLE;
            isStepStarted = true;
            return;
        }

        Timer = 0f;
    }

    void WaitForRealInput(float leftValue, float midValue, float rightValue)
    {
        Timer += Time.unscaledDeltaTime;

        // 왼쪽
        if(direction == Direction.LEFT)
        {
            if(Timer > MaxTimer){
                isKeepPressing = true;
                Timer = 0f;
                SetMatrix();
                AudioManager.instance.PlayClickButtonEffect();
            }
            if(leftValue < threshold * 0.5f)
            {
                isStepStarted = false;

                if(isKeepPressing) 
                {
                    isKeepPressing = false;
                    return; 
                }
                AudioManager.instance.PlayClickButtonEffect();
                SetMatrix();
            }
        }
        // 오른쪽
        if(direction == Direction.RIGHT)
        {
            if(Timer > MaxTimer){
                isKeepPressing = true;
                Timer = 0f;
                AudioManager.instance.PlayClickButtonEffect();
                SetMatrix();
            }
            if(rightValue < threshold * 0.5f)
            {
                isStepStarted = false;

                if(isKeepPressing) 
                {
                    isKeepPressing = false;
                    return; 
                }
                AudioManager.instance.PlayClickButtonEffect();
                SetMatrix();
            }
        }

        // 가운데
        if(direction == Direction.MIDDLE)
        {
            if(midValue < threshold * 0.25f)
            {
                isConfirmClicked = true;
                isStepStarted = false;
                return;
            }
        }
    }
}
