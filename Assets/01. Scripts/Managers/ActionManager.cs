using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static float[,] avgInputMatrix = new float[4,4];

    public EAction action = EAction.WALK;
    ActionSet set = new WalkActionSet();
    public bool isActionDid = false;

    bool isStartAction = false;
    bool isFirstWalkLeft = true;

    public float progress = 0.0f;


    public void ChangeAction(EAction action)
    {
        this.action = action;
        switch(action)
        {
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
        if(isStartAction)
        {
            CheckAction();
            CheckActionSet();
        }
    }

    void CheckAction()
    {
        this.set.action.CheckRep();
    }

    void CheckActionSet()
    {
        this.progress = (this.set.curSet - 1) / this.set.maxSet + 
                        (this.set.curRep / this.set.maxRep) / this.set.maxSet;
        if(this.set.isActionSetEnd())
        {

        }
    }
}

public enum EAction
{
    SQUAT,
    STEPUP,
    WALK,
    PLANK,
    CLIMB
}
