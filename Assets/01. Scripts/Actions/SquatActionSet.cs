using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquatActionSet : ActionSet
{
    public SquatActionSet()
    {
        base.maxSet = maxSet;
        base.maxRep = maxRep;
        base.curSet = curSet;
        base.curRep = curRep;
        base.action = new SquatAction();
        base.action.set = this;
        this.action.InitRep();
    }
    

    public new int maxSet = 10;
    public new int maxRep = 10;

    public new int curSet = 1;
    public new int curRep = 0;
}
