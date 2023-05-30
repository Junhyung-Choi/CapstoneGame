using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO.Ports;
using System.Threading;
using UnityEngine.SceneManagement;

[Serializable]
public class JsonData
{
	public float[] datas = new float[8]; 
}

public class RPInputManager : MonoBehaviour
{
    public bool isTimeOutStarted = false, isTimeOutEnded = false;

    public static RPInputManager instance;
    public static float[,] inputMatrix = new float[2,4];
    public bool isViewerOpen = false;
    
    public TMPro.TMP_Text noticeText;

    SerialPort sp = new SerialPort();
    JsonData data = new JsonData();
    Thread thread;
    bool isArduinoConnected = false;

    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else if(instance != this)
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        
        try{
            sp.PortName = "/dev/cu.usbserial-120";     // 여기에는 아두이노 포트 넣어주면 됩니다.
            sp.BaudRate = 9600;      // 아두이노 보레이트랑 맞춰주시면 됩니다.
            sp.DataBits = 8;
            sp.Parity = Parity.None;
            sp.StopBits = StopBits.One;
            sp.ReadTimeout = 1000;
            sp.Open();    //포트를 엽니다. 열고나면 닫힐동안 시리얼 모니터를 사용하지 못합니다.(여기서 점유하고있으므로)
            isArduinoConnected = true;

            StartCoroutine(StartThread());
        }
        catch (Exception e)
        {
            Debug.Log("아두이노 연결 안됨.", this);
            Debug.LogWarning(e.Message);
        }
    }

    public void ShowNotice(string text)
    {
        noticeText.text = text;
        noticeText.gameObject.SetActive(true);
    }

    

    // Update is called once per frame
    void Update()
    {
        inputMatrix[1,0] = getSensorValue(0);
        inputMatrix[0,0] = getSensorValue(1);
        inputMatrix[1,1] = getSensorValue(2);
        inputMatrix[0,1] = getSensorValue(3);

        inputMatrix[1,2] = getSensorValue(4);
        inputMatrix[0,2] = getSensorValue(5);
        inputMatrix[1,3] = getSensorValue(6);
        inputMatrix[0,3] = getSensorValue(7);

        // print(inputMatrix[0,0] + " " + inputMatrix[1,0] + " " + inputMatrix[0,1] + " " + inputMatrix[1,1] + " " + inputMatrix[0,2] + " " + inputMatrix[1,2] + " " + inputMatrix[0,3] + " " + inputMatrix[1,3] + " ");
        
    }

    float getSensorValue(int sensorNumber)
    {
        switch (sensorNumber)
        {
            case 0:
                if(Input.GetKey(KeyCode.X)) return 1;
                else return data.datas[0];
            case 1:
                if(Input.GetKey(KeyCode.W)) return 1;
                else return data.datas[1];
            case 2:
                if(Input.GetKey(KeyCode.V)) return 1;
                else return data.datas[2];
            case 3:
                if(Input.GetKey(KeyCode.R)) return 1;
                else return data.datas[3];
            case 4:
                if(Input.GetKey(KeyCode.N)) return 1;
                else return data.datas[4];
            case 5:
                if(Input.GetKey(KeyCode.Y)) return 1;
                else return data.datas[5];
            case 6:
                if(Input.GetKey(KeyCode.Comma)) return 1;
                else return data.datas[6];
            case 7:
                if(Input.GetKey(KeyCode.I)) return 1;
                else return data.datas[7];
        }
        return 1;
    }



    private void OnApplicationQuit()
    {
        if(isArduinoConnected)
        {
            sp.Close();    //꺼질때 소켓을 닫아줍니다.
            thread.Abort();
        }
    }

    void ReadValue()
    {
        while(true)
        {
            string line = "";
            try
            {
                line = ReadLine();
                data = JsonUtility.FromJson<JsonData>(line);
                if(isTimeOutStarted) isTimeOutEnded = true;
            }
            catch(TimeoutException e)
            {
                isTimeOutStarted = true;
                Debug.LogWarning(e.Message);
            }
            catch(Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }

    string ReadLine()
    {
        string line = "";
        while(true)
        {
            int readByte = sp.ReadByte();
            if(readByte == 10) break;
            line += (char)readByte;
        }
        return line;
    }

    IEnumerator StartThread()
    {
        thread = new Thread(new ThreadStart(ReadValue));
        thread.IsBackground = true;
        thread.Start();

        yield return null;
    }
}
