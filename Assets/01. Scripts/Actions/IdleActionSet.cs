using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleActionSet:ActionSet
{
    public IdleActionSet()
    {
        this.action = new IdleAction();
        this.action.set = this;
        this.action.InitRep();
    }

    public new Action action = new IdleAction();

    public new int maxSet = 1;
    public new int maxRep = 1;

    public new int curSet = 1;
    public new int curRep = 0;
}
