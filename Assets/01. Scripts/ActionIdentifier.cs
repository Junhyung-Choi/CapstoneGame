using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RPInputManager))]
public class ActionIdentifier : MonoBehaviour
{
    public static Action action = Action.IDLE;
    public static bool isActionDid = false;

    static bool isStartAction = false;
    bool isFirstWalkLeft = true;

    public static void ChangeAction(Action action)
    {
        ActionIdentifier.action = action;
        isStartAction = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAction();
    }

    void CheckAction()
    {
        switch(action)
        {
            case Action.IDLE:
                isActionDid = CheckIdle();
                break;
            case Action.SQUAT:
                isActionDid = CheckSquat();
                break;
            case Action.STEPUP:
                isActionDid = CheckStepup();
                break;
            case Action.WALK:
                isActionDid = CheckWalk();
                break;
            case Action.PLANK:
                isActionDid = CheckPlank();
                break;
            case Action.CLIMB:
                isActionDid = CheckClimb();
                break;
        }
    }

    bool CheckIdle()
    {
        return true;
    }
    
    bool CheckSquat()
    {
        return false;
    }

    bool CheckStepup()
    {
        return false;
    }

    bool CheckWalk()
    {
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
        
        if(isStartAction)
        {
            if(leftValue > rightValue + 1)
            {
                isStartAction = false;
                isFirstWalkLeft = true;
            }
            else if (leftValue + 1 < rightValue)
            {
                isStartAction = false;
                isFirstWalkLeft = false;
            }
        }
        else
        {
            if(isFirstWalkLeft)
            {
                if(leftValue + 1 < rightValue)
                {
                    return true;
                }
            }
            else
            {
                if(leftValue > rightValue + 1)
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    bool CheckPlank()
    {
        return false;
    }

    bool CheckClimb()
    {
        return false;
    }
}

public enum Action
{
    IDLE,
    SQUAT,
    STEPUP,
    WALK,
    PLANK,
    CLIMB
}
