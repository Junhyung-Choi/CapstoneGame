using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingSceneManager : MonoBehaviour
{
    public string userName = "YENA";
    public float userScore = 100f;

    public GameObject rankingCanvas;
    public GameObject RankingInput, InitialBox, RankingOutput;

    List<Record> records = new List<Record>();

    Coroutine coroutine;

    private void Start()
    {
        RankingInput = rankingCanvas.transform.Find("RankingInput").gameObject;
        InitialBox = rankingCanvas.transform.Find("InitialBox").gameObject;
        RankingOutput = rankingCanvas.transform.Find("RankingOutput").gameObject;
    }

    public void Update()
    {
        
    }

    public void CompleteName()
    {
        RankingInput.SetActive(false);
        coroutine = StartCoroutine(ShowRanking());
        RecordManager.AddRecord(new Record(userName, userScore));
        RecordManager.SaveRecord();
    }

    IEnumerator ShowRanking()
    {
        InitialBox.SetActive(true);
        yield return new WaitForSecondsRealtime(3.0f);
        InitialBox.SetActive(false);
        RankingOutput.SetActive(true);
        records = RecordManager.GetRecords();
        SetRankingBoard();
    }

    public void SetRankingBoard()
    {
        records.Sort(delegate (Record a, Record b) {
            if (a.record > b.record) return 1;
            else if (a.record < b.record) return -1;
            else return 0;
        });
        Transform rankingParent = RankingOutput.transform.Find("Ranking");
        for(int i = 0; i < 5; i++)
        {
            if(i >= records.Count) break;
            Transform rankingBox = rankingParent.GetChild(i);
            rankingBox.Find("Name").GetComponent<TMPro.TMP_Text>().text = records[i].userName;
            rankingBox.Find("Time").GetComponent<TMPro.TMP_Text>().text = System.TimeSpan.FromSeconds(records[i].record).ToString(@"mm\:ss");
            rankingBox.gameObject.SetActive(true);
        }
    }

}