using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSet
{
    public ActionSet()
    {
        this.action = new Action();
        this.action.set = this;
        this.action.InitRep();
    }

    public Action action;

    public int maxSet;
    public int maxRep;

    public int curSet = 1;
    public int curRep = 0;

    public bool isActionSetEnd()
    {
        return this.maxSet == this.curSet && this.maxRep == this.curRep;
    }

    public void doRep()
    {
        curRep += 1;
        if(this.maxRep == this.curRep)
        {
            this.curSet += 1;
            this.curRep = 0;
        }
    }

    public void SetAction(Action action)
    {
        this.action = action;
        this.action.set = this;
    }
}
