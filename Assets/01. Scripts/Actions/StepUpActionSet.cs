using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepUpActionSet : ActionSet
{
    public StepUpActionSet()
    {
        base.maxSet = maxSet;
        base.curRep = curRep;
        base.maxRep = maxRep;
        base.curSet = curSet;
        base.action = new StepUpAction();
        base.action.set = this;
        base.action.InitRep();
    }
    public new Action action = new StepUpAction();

    public new int maxSet = 3;
    public new int maxRep = 30;

    public new int curSet = 1;
    public new int curRep = 0;

}
