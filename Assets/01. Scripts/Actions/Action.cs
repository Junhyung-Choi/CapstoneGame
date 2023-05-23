using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public ActionSet set;
    public bool isStarted = false;
    public virtual void CheckRep()
    {
        DoRep();
    }

    public void DoRep()
    {
        this.set.doRep();
    }

    public virtual void InitRep()
    {
        isStarted = false;
    }

    public virtual void StartRep()
    {
        isStarted = true;
    }

    public virtual void _TestSquat(float t)
    {
        throw new System.NotImplementedException();
    }

}
