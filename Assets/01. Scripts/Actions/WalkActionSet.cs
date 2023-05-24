using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkActionSet : ActionSet
{
    public WalkActionSet()
    {
        base.maxSet = maxSet;
        base.curRep = curRep;
        base.maxRep = maxRep;
        base.curSet = curSet;
        base.action = new WalkAction();
        base.action.set = this;
        base.action.InitRep();
    }
    

    public new int maxSet = 1;
    public new int maxRep = 1;

    public new int curSet = 1;
    public new int curRep = 0;
}
