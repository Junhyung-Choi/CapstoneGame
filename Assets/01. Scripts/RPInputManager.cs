using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPInputManager : MonoBehaviour
{
    public static RPInputManager instance;

    public static float[,] inputMatrix = new float[3,8];

    public bool isViewerOpen = false;

    private void Awake() {
        instance = this;  

        DontDestroyOnLoad(this);  
    }

    // Update is called once per frame
    void Update()
    {
        inputMatrix[0,0] = getSensorValue(0);
        inputMatrix[1,0] = getSensorValue(1);
        inputMatrix[1,1] = getSensorValue(2);
        inputMatrix[2,0] = getSensorValue(3);

        inputMatrix[0,2] = getSensorValue(4);
        inputMatrix[1,2] = getSensorValue(5);
        inputMatrix[1,3] = getSensorValue(6);
        inputMatrix[2,2] = getSensorValue(7);

        inputMatrix[0,4] = getSensorValue(8);
        inputMatrix[1,4] = getSensorValue(9);
        inputMatrix[1,5] = getSensorValue(10);
        inputMatrix[2,4] = getSensorValue(11);

        inputMatrix[0,6] = getSensorValue(12);
        inputMatrix[1,6] = getSensorValue(13);
        inputMatrix[1,7] = getSensorValue(14);
        inputMatrix[2,6] = getSensorValue(15);

        if(isViewerOpen)
        {
            ShowViewer();
        }
    }

    float getSensorValue(int sensorNumber)
    {
        switch (sensorNumber)
        {
            case 0:
                if(Input.GetKey(KeyCode.Q)) return 1;
                return 0;
            case 1:
                if(Input.GetKey(KeyCode.A)) return 1;
                return 0;
            case 2:
                if(Input.GetKey(KeyCode.S)) return 1;
                return 0;
            case 3:
                if(Input.GetKey(KeyCode.Z)) return 1;
                return 0;
            case 4:
                if(Input.GetKey(KeyCode.E)) return 1;
                return 0;
            case 5:
                if(Input.GetKey(KeyCode.D)) return 1;
                return 0;
            case 6:
                if(Input.GetKey(KeyCode.F)) return 1;
                return 0;
            case 7:
                if(Input.GetKey(KeyCode.C)) return 1;
                return 0;
            case 8:
                if(Input.GetKey(KeyCode.T)) return 1;
                return 0;
            case 9:
                if(Input.GetKey(KeyCode.G)) return 1;
                return 0;
            case 10:
                if(Input.GetKey(KeyCode.H)) return 1;
                return 0;
            case 11:
                if(Input.GetKey(KeyCode.B)) return 1;
                return 0;
            case 12:
                if(Input.GetKey(KeyCode.U)) return 1;
                return 0;
            case 13:
                if(Input.GetKey(KeyCode.J)) return 1;
                return 0;
            case 14:
                if(Input.GetKey(KeyCode.K)) return 1;
                return 0;
            case 15:
                if(Input.GetKey(KeyCode.N)) return 1;
                return 0;
        }
        return 1;
    }

    void ShowViewer()
    {
        Transform canvas = transform.GetChild(0);
        if(canvas.gameObject.activeSelf == false) canvas.gameObject.SetActive(true);
        Transform inputViewer = canvas.GetChild(0);

        inputViewer.GetChild(0).GetComponent<Image>().color = new Color(inputMatrix[0,0],0,0);
        inputViewer.GetChild(1).GetComponent<Image>().color = new Color(inputMatrix[1,0],0,0);
        inputViewer.GetChild(2).GetComponent<Image>().color = new Color(inputMatrix[1,1],0,0);
        inputViewer.GetChild(3).GetComponent<Image>().color = new Color(inputMatrix[2,0],0,0);

        inputViewer.GetChild(4).GetComponent<Image>().color = new Color(inputMatrix[0,2],0,0);
        inputViewer.GetChild(5).GetComponent<Image>().color = new Color(inputMatrix[1,2],0,0);
        inputViewer.GetChild(6).GetComponent<Image>().color = new Color(inputMatrix[1,3],0,0);
        inputViewer.GetChild(7).GetComponent<Image>().color = new Color(inputMatrix[2,2],0,0);

        inputViewer.GetChild(8).GetComponent<Image>().color = new Color(inputMatrix[0,4],0,0);
        inputViewer.GetChild(9).GetComponent<Image>().color = new Color(inputMatrix[1,4],0,0);
        inputViewer.GetChild(10).GetComponent<Image>().color = new Color(inputMatrix[1,5],0,0);
        inputViewer.GetChild(11).GetComponent<Image>().color = new Color(inputMatrix[2,4],0,0);

        inputViewer.GetChild(12).GetComponent<Image>().color = new Color(inputMatrix[0,6],0,0);
        inputViewer.GetChild(13).GetComponent<Image>().color = new Color(inputMatrix[1,6],0,0);
        inputViewer.GetChild(14).GetComponent<Image>().color = new Color(inputMatrix[1,7],0,0);
        inputViewer.GetChild(15).GetComponent<Image>().color = new Color(inputMatrix[2,6],0,0);
    }
}
