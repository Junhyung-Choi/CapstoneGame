using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Threading;

[Serializable]
public class JsonData
{
	public float[] datas = new float[8]; 
}

public class RPInputManager : MonoBehaviour
{
    public static RPInputManager instance;
    public static float[,] inputMatrix = new float[4,4];
    public bool isViewerOpen = false;
    SerialPort sp = new SerialPort();
    JsonData data = new JsonData();
    Thread thread;

    private void Awake() {
        instance = this;  

        DontDestroyOnLoad(this);  
    }

    void Start()
    {
        sp.PortName = "/dev/cu.usbmodem21201";     // 여기에는 아두이노 포트 넣어주면 됩니다.
        sp.BaudRate = 9600;      // 아두이노 보레이트랑 맞춰주시면 됩니다.
        sp.DataBits = 8;
        sp.Parity = Parity.None;
        sp.StopBits = StopBits.One;
        sp.Open();    //포트를 엽니다. 열고나면 닫힐동안 시리얼 모니터를 사용하지 못합니다.(여기서 점유하고있으므로)

        thread = new Thread(new ThreadStart(ReadValue));
        thread.IsBackground = true;
        thread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        inputMatrix[0,0] = getSensorValue(0);
        inputMatrix[0,1] = getSensorValue(1);
        inputMatrix[0,2] = getSensorValue(2);
        inputMatrix[0,3] = getSensorValue(3);

        inputMatrix[1,0] = getSensorValue(4);
        inputMatrix[1,1] = getSensorValue(5);
        inputMatrix[1,2] = getSensorValue(6);
        inputMatrix[1,3] = getSensorValue(7);

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
                if(Input.GetKey(KeyCode.W)) return 10;
                else return data.datas[0];
            case 1:
                if(Input.GetKey(KeyCode.R)) return 1;
                return 0;
            case 2:
                if(Input.GetKey(KeyCode.Y)) return 1;
                return 0;
            case 3:
                if(Input.GetKey(KeyCode.I)) return 1;
                return 0;
            case 4:
                if(Input.GetKey(KeyCode.X)) return 1;
                return 0;
            case 5:
                if(Input.GetKey(KeyCode.V)) return 1;
                return 0;
            case 6:
                if(Input.GetKey(KeyCode.N)) return 1;
                return 0;
            case 7:
                if(Input.GetKey(KeyCode.Comma)) return 1;
                return 0;
        }
        return 1;
    }

    void ShowViewer()
    {
        Transform canvas = transform.GetChild(0);
        if(canvas.gameObject.activeSelf == false) canvas.gameObject.SetActive(true);
        Transform inputViewer = canvas.GetChild(0);

        inputViewer.GetChild(0).GetComponent<Image>().color = new Color(Math.Abs(inputMatrix[0,0]/10),0,0);
        inputViewer.GetChild(1).GetComponent<Image>().color = new Color(Math.Abs(inputMatrix[0,1]/10),0,0);
        inputViewer.GetChild(2).GetComponent<Image>().color = new Color(Math.Abs(inputMatrix[0,2]/10),0,0);
        inputViewer.GetChild(3).GetComponent<Image>().color = new Color(Math.Abs(inputMatrix[0,3]/10),0,0);

        inputViewer.GetChild(4).GetComponent<Image>().color = new Color(Math.Abs(inputMatrix[1,0]/10),0,0);
        inputViewer.GetChild(5).GetComponent<Image>().color = new Color(Math.Abs(inputMatrix[1,1]/10),0,0);
        inputViewer.GetChild(6).GetComponent<Image>().color = new Color(Math.Abs(inputMatrix[1,2]/10),0,0);
        inputViewer.GetChild(7).GetComponent<Image>().color = new Color(Math.Abs(inputMatrix[1,3]/10),0,0);
    }

    private void OnApplicationQuit()
    {
        sp.Close();    //꺼질때 소켓을 닫아줍니다.
        thread.Abort();
    }

    void ReadValue()
    {
        while(true)
        {
            try
            {
                string line = sp.ReadLine();
                Debug.Log(line);
                data = JsonUtility.FromJson<JsonData>(line);
            }
            catch
            {

            }
        }
    }
}
