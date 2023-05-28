using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static float[,] avgInputMatrix = new float[2,4];

    ActionSet set = new WalkActionSet();

    int beforeRep = 0;

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
        beforeRep = 0;
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
        Debug.Log("curRep : " + this.set.curRep);
        Debug.Log("beforeRep : " + beforeRep);
        if(beforeRep != this.set.curRep)
        {
            beforeRep = this.set.curRep;
            GameManager.instance.DoRep();
        }
    }
}