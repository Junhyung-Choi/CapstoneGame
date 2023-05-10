using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RPInputManager))]
public class ActionManager : MonoBehaviour
{
    public EAction action = EAction.IDLE;
    ActionSet set = new IdleActionSet();
    public bool isActionDid = false;

    bool isStartAction = false;
    bool isFirstWalkLeft = true;

    public float progress = 0.0f;

    public void ChangeAction(EAction action)
    {
        this.action = action;
        switch(action)
        {
            case EAction.IDLE:
                this.set = new IdleActionSet();
                break;
            case EAction.SQUAT:
                this.set = new SquatActionSet();
                break;
            case EAction.STEPUP:
                this.set = new StepUpActionSet();
                break;
            case EAction.WALK:
                this.set = new WalkActionSet();
                break;
            case EAction.PLANK:
                this.set = new PlankActionSet();
                break;
            case EAction.CLIMB:
                this.set = new ClimbActionSet();
                break;
        }
        this.set.action.StartRep();
    }

    // Update is called once per frame
    void Update()
    {
        CheckAction();
        CheckActionSet();
    }

    void CheckAction()
    {
        this.set.action.CheckRep();
    }

    void CheckActionSet()
    {
        this.progress = (this.set.curSet - 1) / this.set.maxSet + 
                        (this.set.curRep / this.set.maxRep) / this.set.maxSet;
        this.set.isActionSetEnd();
    }
}

public enum EAction
{
    IDLE,
    SQUAT,
    STEPUP,
    WALK,
    PLANK,
    CLIMB
}
