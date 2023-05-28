using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealStartSceneMover : SceneMover
{
    float totalValue;

    private void Update() {
        totalValue = 0f;
        for(int i = 0; i < 4; i++)
        {
            totalValue += RPInputManager.inputMatrix[0,i];
            totalValue += RPInputManager.inputMatrix[1,i];
        }

        if(totalValue > 5f)
            MoveToSetting();
    }
}
