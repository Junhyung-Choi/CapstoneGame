using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RPInputManager))]
public class ActionIdentifier : MonoBehaviour
{
    public static Action action = Action.IDLE;

    public static void ChangeAction(Action action)
    {
        ActionIdentifier.action = action;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Action
{
    IDLE,
    SQUAT,
    STEPUP,
    WALK,
    PLANK,
    CLIMB
}
