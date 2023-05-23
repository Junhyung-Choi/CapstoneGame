using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static float[,] avgInputMatrix = new float[4,4];

    ActionSet set = new WalkActionSet();
    public bool isActionDid = false;

    bool isStartAction = false;
    bool isFirstWalkLeft = true;

    public float progress = 0.0f;


    public void ChangeAction(ChunkType action)
    {
        switch(action)
        {
            case ChunkType.WALK:
                this.set = new WalkActionSet();
                break;
            case ChunkType.SQUAT:
                this.set = new SquatActionSet();
                break;
            case ChunkType.STEPUP:
                this.set = new StepUpActionSet();
                break;
            case ChunkType.PLANK:
                this.set = new PlankActionSet();
                break;
            case ChunkType.CLIMB:
                this.set = new ClimbActionSet();
                break;
            case ChunkType.START:
                this.set = new WalkActionSet();
                break;
            case ChunkType.END:
                this.set = new WalkActionSet();
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