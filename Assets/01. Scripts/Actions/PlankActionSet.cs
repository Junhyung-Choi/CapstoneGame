using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlankActionSet: ActionSet
{
    public PlankActionSet()
    {
        this.action = new PlankAction();
        this.action.set = this;
        this.action.InitRep();
    }
    public new Action action = new PlankAction();

    public new int maxSet = 5;
    public new int maxRep = 1;

    public new int curSet = 1;
    public new int curRep = 0;
}
