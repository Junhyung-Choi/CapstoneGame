using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbActionSet:ActionSet
{
    public ClimbActionSet()
    {
        base.maxSet = maxSet;
        base.curRep = curRep;
        base.maxRep = maxRep;
        base.curSet = curSet;
        this.action = new ClimbAction();
        this.action.set = this;
        this.action.InitRep();
    }

    public new Action action = new ClimbAction();

    public new int maxSet = 3;
    public new int maxRep = 1;

    public new int curSet = 1;
    public new int curRep = 0;
}
