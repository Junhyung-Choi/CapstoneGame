using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    bool isMale = true;
    int userAge, userWeight;

    float leftValue, middleValue, rightValue;

    bool isKeepPressing = false;
    float qunatifyTimer = 0.0f, quantifyingTime = 1.0f;

    float keepPressTimer = 0.0f;

    float threshold = 10f;
    float diff = 1.0f;

    string[] modes = {"SetSex", "SetAge", "SetWeight"};
    int modeIndex;

    GameObject instructionCanvasObject;
    Text instructionText, valueText;

    private void Start() {
        instructionCanvasObject = GameObject.Find("Instruction Canvas");
        instructionText = instructionCanvasObject.transform.Find("Instruction").GetComponent<Text>();
        valueText = instructionCanvasObject.transform.Find("Value").GetComponent<Text>();

        userAge = PlayerPrefs.GetInt("UserAge",20);
        userWeight = PlayerPrefs.GetInt("UserWeight",50);
        
        if(PlayerPrefs.GetInt("UserSex",0) == 0) { isMale = false; } 
        else                                    { isMale = true; }
        
        modeIndex = 0;
    }

    private void Update() {
        leftValue = GetLeftValue();
        rightValue = GetRightValue();
        middleValue = GetMiddleValue();
        
        SetSetting(leftValue, rightValue, middleValue);
        SetText();
    }

    private void SetSetting(float leftValue, float rightValue, float middleValue)
    {
        // 계속 누르고 있을수록 빠르게 값을 변경하기 위한 코드
        keepPressTimer += Time.deltaTime * 0.5f;
        if(keepPressTimer > 0.6f)
        {
            keepPressTimer = 0.6f;
        }

        // 입력이 없을 시, 중복 입력 방지 타이머와, 지속 입력 타이머 초기화
        if(leftValue < 2f && rightValue < 2f && middleValue < 4f)
        {
            isKeepPressing = false;
            qunatifyTimer = 0.0f;
            keepPressTimer = 0.0f;
        }
        
        switch(modeIndex)
        {
            case 0:
                SetSex();
                break;
            case 1:
                SetAge();
                break;
            case 2:
                SetWeight();
                break;
            case 3:
                SceneManager.LoadScene("Start");
                break;
            default:
                Debug.LogError("SettingManager.cs: SetSetting() - modeIndex Error");
                break;
        }
        
        // 지속 입력시 연속 입력 기능을 위한 타이머 체크.
        if(isKeepPressing)
        {
            qunatifyTimer += Time.deltaTime;
            if((quantifyingTime - keepPressTimer) < qunatifyTimer)
            {
                isKeepPressing = false;
                qunatifyTimer = 0.0f;
            }
        }
    }

    void SetSex() 
    {
        // 가운데를 누르면 다음 모드로 넘어감
        if(threshold < middleValue && !isKeepPressing)
        {
            isKeepPressing = true;
            qunatifyTimer = 0.0f;
            keepPressTimer = 0.0f;
            if(isMale) { PlayerPrefs.SetInt("UserSex", 1); }
            else       { PlayerPrefs.SetInt("UserSex", 0); }
            modeIndex += 1;
        }
        // 오른쪽을 누르면 값이 증가함.
        if(leftValue + diff < rightValue && !isKeepPressing)
        {
            isKeepPressing = true;
            isMale = !isMale;
        }
        // 왼쪽을 누르면 값이 감소함.
        if(rightValue + diff < leftValue && !isKeepPressing)
        {
            isKeepPressing = true;
            isMale = !isMale;
        }
}

    void SetAge() 
    {
        if(threshold < middleValue && !isKeepPressing)
        {
            isKeepPressing = true;
            qunatifyTimer = 0.0f;
            keepPressTimer = 0.0f;
            PlayerPrefs.SetInt("UserAge", userAge);
            modeIndex += 1;
        }
        if(leftValue + diff < rightValue && !isKeepPressing)
        {
            isKeepPressing = true;
            userAge += 1;
        }
        if(rightValue + diff < leftValue && !isKeepPressing)
        {
            isKeepPressing = true;
            userAge -= 1;
        }
    }

    void SetWeight() 
    {
        if(threshold < middleValue && !isKeepPressing)
        {
            isKeepPressing = true;
            qunatifyTimer = 0.0f;
            keepPressTimer = 0.0f;
            PlayerPrefs.SetInt("UserWeight", userWeight);
            modeIndex += 1;
        }
        if(leftValue + diff < rightValue && !isKeepPressing)
        {
            isKeepPressing = true;
            userWeight += 1;
        }
        if(rightValue + diff < leftValue && !isKeepPressing)
        {
            isKeepPressing = true;
            userWeight -= 1;
        }
    }

    void SetText()
    {
        switch(modeIndex)
        {
            case 0:
                instructionText.text = "성별을 설정하세요!";
                valueText.text = isMale ? "남성" : "여성";
                break;
            case 1:
                instructionText.text = "나이를 설정하세요!";
                valueText.text = userAge.ToString();
                break;
            case 2:
                instructionText.text = "몸무게를 설정하세요!";
                valueText.text = userWeight.ToString();
                break;
            default:
                Debug.LogError("SettingManager.cs: SetText() - modeIndex Error");
                break;
        }
    }

    float GetLeftValue()
    {
        float value = 
            RPInputManager.inputMatrix[0,0] +
            RPInputManager.inputMatrix[1,0];
        return value;
    }

    float GetRightValue()
    {
        float value = 
            RPInputManager.inputMatrix[0,3] + 
            RPInputManager.inputMatrix[1,3];
        return value;
    }

    float GetMiddleValue()
    {
        float value = 
            RPInputManager.inputMatrix[0,1] +
            RPInputManager.inputMatrix[1,1] +
            RPInputManager.inputMatrix[0,2] + 
            RPInputManager.inputMatrix[1,2];
        return value;
    }


}
