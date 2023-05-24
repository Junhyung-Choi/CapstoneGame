using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialChecker : SceneMover
{
    public GameObject tutorialUI;
    Slider slider;
    Text text;

    float rightStepTimer = 0f;
    float rightStepMaxTime = 5f;
    float rightStepThreshold = 0f;
    float rightStepThresholdRate = 0.8f;

    int tutoCount = 0;
    int maxTutoCount = 5; // Walk, StepUp, Plank, Sqaut, ArmWalk

    bool isTutorialEnd = false;
    bool isActionSetEnd = false;

    List<ChunkType> types = new List<ChunkType>();

    Action action;
    ActionSet actionSet;

    private void Start() {
        slider = tutorialUI.transform.Find("Slider").GetComponent<Slider>();
        text = tutorialUI.transform.Find("Instruction").GetComponent<Text>();

        types.Add(ChunkType.WALK);
        types.Add(ChunkType.STEPUP);
        types.Add(ChunkType.SQUAT);
        types.Add(ChunkType.PLANK);
        types.Add(ChunkType.CLIMB);

        actionSet = new WalkActionSet();

        slider.maxValue = rightStepMaxTime;

        SetRightStepThreshold();
    }
    
    private void Update() {
        ControlTutorial();
        CheckTutorialEnd();
    }

    public void ControlTutorial()
    {
        if(tutoCount >= maxTutoCount) {     
            return; 
        }

        if(actionSet == null) { return; }

        if(isActionSetEnd)
        {
            tutoCount += 1;
            actionSet = GetActionSet(types[tutoCount]);
            text.text = ChangeTutorialMent();
            isActionSetEnd = false;
        }
    }

    string ChangeTutorialMent()
    {
        switch(this.types[tutoCount])
        {
            case ChunkType.WALK:
                return "걷기를 해보세요!";
            case ChunkType.SQUAT:
                return "스쿼트를 해보세요!";
            case ChunkType.STEPUP:
                return "스텝업을 해보세요!";
            case ChunkType.PLANK:
                return "플랭크를 해보세요!";
            case ChunkType.CLIMB:
                return "클라이밍을 해보세요!";
            default:
                throw new System.Exception("Wrong ChunkType");
        }
    }

    public void DoAction()
    {
        if(!this.isActionSetEnd) { this.actionSet.action.CheckRep(); }

        if(this.actionSet.curRep >= 3)
        {
            isActionSetEnd = true;
        }
    }

    ActionSet GetActionSet(ChunkType type)
    {
        switch(type)
        {
            case ChunkType.WALK:
                return new WalkActionSet();
            case ChunkType.SQUAT:
                return new SquatActionSet();
            case ChunkType.STEPUP:
                return new StepUpActionSet();
            case ChunkType.PLANK:
                return new PlankActionSet();
            case ChunkType.CLIMB:
                return new ClimbActionSet();
            default:
                throw new System.Exception("Wrong ChunkType");
        }
    }

    void CheckTutorialEnd()
    {
        if(CheckRightStep() || isTutorialEnd) // Check Right Step
        {
            rightStepTimer += Time.deltaTime;
            slider.value = rightStepTimer;

            if(rightStepTimer > rightStepMaxTime) { this.MoveToGame(); }
        }
        else 
        { 
            rightStepTimer = 0; 
            slider.value = 0f;
        }
    }

    bool CheckRightStep()
    {
        float sum = 0f;
        
        sum = RPInputManager.inputMatrix[1,2] + 
              RPInputManager.inputMatrix[0,2] + 
              RPInputManager.inputMatrix[1,3] + 
              RPInputManager.inputMatrix[0,3];

        if(sum < rightStepThreshold) { return false; }

        return true;
    }


    void SetRightStepThreshold()
    {
        float sum = 0f;

        for(int i = 0; i < ActionManager.avgInputMatrix.GetLength(0); i++)
        {
            for(int j = 0; j < ActionManager.avgInputMatrix.GetLength(1); j++)
            {
                sum += ActionManager.avgInputMatrix[i,j];
            }
        }

        // rightStepThreshold = sum * rightStepThresholdRate;
        rightStepThreshold = 0.5f;
    }
}
