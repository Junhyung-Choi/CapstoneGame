using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkActionSet : ActionSet
{
    public WalkActionSet()
    {
        this.action = new WalkAction();
        this.action.set = this;
        this.action.InitRep();
    }
    
    public new Action action = new WalkAction();

    public new int maxSet = 1;
    public new int maxRep = 1;

    public new int curSet = 1;
    public new int curRep = 0;
}
