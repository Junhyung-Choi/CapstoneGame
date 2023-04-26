using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquatActionSet : ActionSet
{
    public SquatActionSet()
    {
        this.action = new SquatAction();
        this.action.set = this;
        this.action.InitRep();
    }
    
    public new Action action = new SquatAction();

    public new int maxSet = 3;
    public new int maxRep = 12;

    public new int curSet = 1;
    public new int curRep = 0;
}
