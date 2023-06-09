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
        base.action = new ClimbAction();
        base.action.set = this;
        this.action.InitRep();
    }

    public new int maxSet = 10;
    public new int maxRep = 10;

    public new int curSet = 1;
    public new int curRep = 0;
}
