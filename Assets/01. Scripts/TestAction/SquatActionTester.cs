using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SquatActionTester : MonoBehaviour
{
    [SerializeField]
    public Slider slider;

    ActionSet set;
    Action action;

    float initTimer = 0.0f;
    float initTime = 10f;

    public void Start()
    {
        set = new SquatActionSet();
        // action = new SquatAction();
        // set.SetAction(action);
        this.set.action.StartRep();
    }

    public void Update()
    {
        this.set.action.CheckRep();
        slider.value = RPInputManager.inputMatrix[1,2] + RPInputManager.inputMatrix[0,3];
        Debug.Log(this.set.curRep);
        // float progress = (this.set.curSet - 1) / this.set.maxSet + 
                        // (this.set.curRep / this.set.maxRep) / this.set.maxSet;
        // Debug.Log(progress);
    }

    public IEnumerator SetThreshold()
    {
        float t = 0.0f;
        float timer = 0;
        float maxTime = 10f;
        double sum = 0;
        double count = 0;
        while(true)
        {
            timer += Time.deltaTime;
            sum += (RPInputManager.inputMatrix[1,2] + RPInputManager.inputMatrix[0,3]);
            count += 1;
            if(timer > maxTime)
            {
                break;
            }
            yield return new WaitForSeconds(0.005f);
        }
        double avg = sum / count;
        t = (float)avg;
        Debug.Log(t);
        this.set.action._TestSquat(t);
        // this.set.action.InitRep();
        slider.minValue = t;
        slider.maxValue = t * 1.2f;
        yield return null;
    }

    public void SetThresholdButton()
    {
        StartCoroutine(SetThreshold());
    }
    
}
