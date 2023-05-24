using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlankActionTester : MonoBehaviour
{
    public GameObject instruction;
    Text instructionText;

    ActionSet set;
    Action action;

    float initTimer = 0.0f;
    float initTime = 10f;

    public void Start()
    {
        set = new PlankActionSet();
        // action = new SquatAction();
        // set.SetAction(action);
        this.set.action.StartRep();
        instructionText = instruction.GetComponent<Text>();
    }

    public void Update()
    {
        this.set.action.CheckRep();
        instructionText.text = this.set.curRep.ToString();
        Debug.Log(this.set.curRep);
        // float progress = (this.set.curSet - 1) / this.set.maxSet + 
                        // (this.set.curRep / this.set.maxRep) / this.set.maxSet;
        // Debug.Log(progress);
    }

    public IEnumerator SetThreshold()
    {
        Debug.Log(10);
        this.set.action._TestSquat(10);
        // this.set.action.InitRep();
        yield return null;
    }

    public void SetThresholdButton()
    {
        StartCoroutine(SetThreshold());
    }
}
