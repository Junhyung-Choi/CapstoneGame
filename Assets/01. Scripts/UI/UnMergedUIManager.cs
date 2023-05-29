using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UnMergedUIManager : MonoBehaviour
{
    Transform canvas;
    private void Update() {
        canvas = this.transform;
    }

    void ShowViewer()
    {
        Transform inputViewer = canvas.GetChild(0);

        inputViewer.GetChild(0).GetComponent<Image>().color = new Color(Math.Abs(RPInputManager.inputMatrix[0,0] * 0.05f),0,0);
        inputViewer.GetChild(1).GetComponent<Image>().color = new Color(Math.Abs(RPInputManager.inputMatrix[0,1] * 0.05f),0,0);
        inputViewer.GetChild(2).GetComponent<Image>().color = new Color(Math.Abs(RPInputManager.inputMatrix[0,2] * 0.05f),0,0);
        inputViewer.GetChild(3).GetComponent<Image>().color = new Color(Math.Abs(RPInputManager.inputMatrix[0,3] * 0.05f),0,0);

        inputViewer.GetChild(4).GetComponent<Image>().color = new Color(Math.Abs(RPInputManager.inputMatrix[1,0] * 0.05f),0,0);
        inputViewer.GetChild(5).GetComponent<Image>().color = new Color(Math.Abs(RPInputManager.inputMatrix[1,1] * 0.05f),0,0);
        inputViewer.GetChild(6).GetComponent<Image>().color = new Color(Math.Abs(RPInputManager.inputMatrix[1,2] * 0.05f),0,0);
        inputViewer.GetChild(7).GetComponent<Image>().color = new Color(Math.Abs(RPInputManager.inputMatrix[1,3] * 0.05f),0,0);
    }
}
