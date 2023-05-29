using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public static RecordManager instance;

    public static List<Record> records = new List<Record>();

    string persistenPath;

    private void Awake() {
        persistenPath = Application.persistentDataPath;

        if(instance == null) { instance = this; }
        else { Destroy(gameObject); }

        LoadRecord();
    }

    private void OnApplicationQuit() {
        SaveRecord();
    }

    public static List<Record> GetRecords()
    {
        return records;
    }

    public static void AddRecord(Record record)
    {
        records.Add(record);
    }

    void SaveRecord()
    {
        string json = JsonUtility.ToJson(records);
        System.IO.File.WriteAllText(persistenPath + "/record.json", json);
    }

    void LoadRecord()
    {
        string json = System.IO.File.ReadAllText(persistenPath + "/record.json");
        records = JsonUtility.FromJson<List<Record>>(json);
    }
    
}

[System.Serializable]
public class Record
{
    public string userName;
    public float record;
}
