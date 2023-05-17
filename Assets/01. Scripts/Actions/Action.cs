using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public ActionSet set;
    public bool isStarted = false;
    public float threshold = 30f;
    public bool isThresholdSet = false;
    public virtual void CheckRep()
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

    public virtual void SetThreshold(float threshold)
    {
        this.threshold = threshold;
        isThresholdSet = true;
    }
}
