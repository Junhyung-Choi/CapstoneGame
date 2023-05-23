using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    int userAge;
    int userWeight;
    bool quantified = false;
    float qunatifyTimer = 0.0f;
    float quantifyingTime = 1.0f;
    float succeedingPressTimer = 0.0f;

    float confirmValue = 2.5f;
    float diff = 1.0f;

    string[] modes = {"SetAge", "SetWeight"};
    int modeIndex;

    private void Start() {
        userAge = PlayerPrefs.GetInt("UserAge",20);
        userWeight = PlayerPrefs.GetInt("UserWeight",50);
        modeIndex = 0;
    }

    private void Update() {
        float leftValue = 
            RPInputManager.inputMatrix[0,0] +
            RPInputManager.inputMatrix[1,0];

        float rightValue = 
            RPInputManager.inputMatrix[0,3] + 
            RPInputManager.inputMatrix[1,3];

        float middleValue = 
            RPInputManager.inputMatrix[0,1] +
            RPInputManager.inputMatrix[1,1] +
            RPInputManager.inputMatrix[0,2] + 
            RPInputManager.inputMatrix[1,2];
        
        setValue(leftValue, rightValue, middleValue);
        setUI();
    }

    private void setValue(float leftValue, float rightValue, float middleValue)
    {
        // 계속 누르고 있을수록 빠르게 값을 변경하기 위한 코드
        succeedingPressTimer += Time.deltaTime * 0.5f;
        if(succeedingPressTimer > 0.9f)
        {
            succeedingPressTimer = 0.9f;
        }

        // 입력이 없을 시, 중복 입력 방지 타이머와, 지속 입력 타이머 초기화
        if(leftValue < 0.01f && rightValue < 0.01f && middleValue < 0.01f)
        {
            quantified = false;
            qunatifyTimer = 0.0f;
            succeedingPressTimer = 0.0f;
        }

        // 나이 설정 모드일때
        if(modeIndex == 0)
        {
            // 가운데를 누르면 다음 모드로 넘어감
            if(confirmValue < middleValue && !quantified)
            {
                quantified = true;
                PlayerPrefs.SetInt("UserAge", userAge);
                modeIndex = 1;
            }
            // 오른쪽을 누르면 값이 증가함.
            if(leftValue + diff < rightValue && !quantified)
            {
                quantified = true;
                userAge += 1;
            }
            // 왼쪽을 누르면 값이 감소함.
            if(rightValue + diff < leftValue && !quantified)
            {
                quantified = true;
                userAge -= 1;
            }
            
        }
        // 무게 설정 모드일때
        // 이하 동일
        else if(modeIndex == 1)
        {
            if(confirmValue < middleValue && !quantified)
            {
                quantified = true;
                PlayerPrefs.SetInt("UserWeight", userWeight);
                modeIndex += 1;
            }
            if(leftValue + diff < rightValue && !quantified)
            {
                quantified = true;
                userWeight += 1;
            }
            if(rightValue + diff < leftValue && !quantified)
            {
                quantified = true;
                userWeight -= 1;
            }
        }
        // 무게 설정 모드에서 넘어가면 바로 씬 이동.
        else if (modeIndex > 1)
        {
            SceneManager.LoadScene("Start");
        }
        
        // 지속 입력시 연속 입력 기능을 위한 타이머 체크.
        if(quantified)
        {
            qunatifyTimer += Time.deltaTime;
            if((quantifyingTime - succeedingPressTimer) < qunatifyTimer)
            {
                quantified = false;
                qunatifyTimer = 0.0f;
            }
        }
}

    private void setUI()
    {
        GameObject instructionCanvasObject = GameObject.Find("Instruction Canvas");
        Text instructionText = instructionCanvasObject.transform.Find("Instruction").GetComponent<Text>();
        Text valueText = instructionCanvasObject.transform.Find("Value").GetComponent<Text>();

        if(modeIndex == 0)
        {
            instructionText.text = "나이를 설정하세요!";
            valueText.text = userAge.ToString();
        }
        else if (modeIndex == 1)
        {
            instructionText.text = "몸무게를 설정하세요!";
            valueText.text = userWeight.ToString();
        }
    }
}
