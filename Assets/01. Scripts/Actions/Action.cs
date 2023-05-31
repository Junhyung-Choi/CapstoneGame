using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public ActionSet set;
    public bool isStarted = false;

    public float progress = 0f, destProgress = 0f;
    
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

    public IEnumerator SetProgress(float t)
    {
        float time = 0;

        while (time <= 0.5)
        {
            progress = Mathf.Lerp(progress, t, time * 2);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
    }

}
