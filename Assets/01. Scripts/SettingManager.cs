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
            RPInputManager.inputMatrix[0,1] +
            RPInputManager.inputMatrix[1,0] + 
            RPInputManager.inputMatrix[1,1];

        float rightValue = 
            RPInputManager.inputMatrix[0,2] + 
            RPInputManager.inputMatrix[0,3] + 
            RPInputManager.inputMatrix[1,2] + 
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
        if(leftValue < 0.01f && rightValue < 0.01f)
        {
            quantified = false;
            qunatifyTimer = 0.0f;
        }

        if(modeIndex == 0)
        {
            if(2.5 < middleValue && !quantified)
            {
                quantified = true;
                PlayerPrefs.SetInt("UserAge", userAge);
                modeIndex = 1;
            }
            if(leftValue + 1 < rightValue && !quantified)
            {
                quantified = true;
                userAge += 1;
            }
            if(rightValue + 1 < leftValue && !quantified)
            {
                quantified = true;
                userAge -= 1;
            }
            if(quantified)
            {
                qunatifyTimer += Time.deltaTime;
                if(quantifyingTime < qunatifyTimer)
                {
                    quantified = false;
                    qunatifyTimer = 0.0f;
                }
            }
        }
        else if(modeIndex == 1)
        {
            if(2.5 < middleValue && !quantified)
            {
                quantified = true;
                PlayerPrefs.SetInt("UserWeight", userWeight);
                modeIndex += 1;
            }
            if(leftValue + 1 < rightValue && !quantified)
            {
                quantified = true;
                userWeight += 1;
            }
            if(rightValue + 1 < leftValue && !quantified)
            {
                quantified = true;
                userWeight -= 1;
            }
            if(quantified)
            {
                qunatifyTimer += Time.deltaTime;
                if(quantifyingTime < qunatifyTimer)
                {
                    quantified = false;
                    qunatifyTimer = 0.0f;
                }
            }
        }
        else if (modeIndex > 1)
        {
            SceneManager.LoadScene("Start");
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
