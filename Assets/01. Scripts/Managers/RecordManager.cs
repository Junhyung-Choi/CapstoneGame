using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public static RecordManager instance;

    public static List<Record> records = new List<Record>();

    static string persistenPath;

    private void Awake() {
        persistenPath = Application.persistentDataPath;


        if(instance == null) { instance = this; }
        else { Destroy(gameObject); }

        LoadRecord();
    }

    private void OnApplicationQuit() {
        SaveRecord();
        Debug.Log(persistenPath);
    }

    public static List<Record> GetRecords()
    {
        return records;
    }

    public static void AddRecord(Record record)
    {
        records.Add(record);
        for(int i = 0; i < records.Count; i++)
        {
            Debug.Log(records[i].userName + " " + records[i].record);
        }
    }

    public static void SaveRecord()
    {
        RecordList recordList = new RecordList();
        recordList.records = records;
        string json = JsonUtility.ToJson(recordList);
        Debug.Log(json);
        System.IO.File.WriteAllText(persistenPath + "/record.json", json);
    }

    void LoadRecord()
    {
        string json = System.IO.File.ReadAllText(persistenPath + "/record.json");
        RecordList recordList = JsonUtility.FromJson<RecordList>(json);
        records = recordList.records;
    }
    
}

[System.Serializable]
public class Record
{
    public string userName;
    public float record;

    public Record(string userName, float record)
    {
        this.userName = userName;
        this.record = record;
    }
}

[System.Serializable]
public class RecordList
{
    public List<Record> records;
}
