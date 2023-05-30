using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartUIManager : MonoBehaviour
{
    TMPro.TMP_Text head, body;
    GameObject warning;
    
    // Start is called before the first frame update
    void Start()
    {
        head = this.transform.Find("GuideBox").Find("Head").GetComponent<TMPro.TMP_Text>();
        body = this.transform.Find("GuideBox").Find("Body").GetComponent<TMPro.TMP_Text>();

        warning = this.transform.Find("Feedback").gameObject;
    }

    public void SetInstructionMent(string ment)
    {
        body.gameObject.SetActive(false);
        head.text = ment;
    }

    public void SetNoticeMent(string head, string body)
    {
        this.head.text = head;
        this.body.text = body;
        this.body.gameObject.SetActive(true);
    }
}
