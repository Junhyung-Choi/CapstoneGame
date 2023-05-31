using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionManager : MonoBehaviour
{
    Image progress;
    TMPro.TMP_Text startWaitTimerText, playTimerText;

    Fade initNoticeFade;

    public bool isInitNoticeFaded = false;

    public void SetPauseActive(bool isActive)
    {
        transform.Find("Pause").gameObject.SetActive(isActive);
    }

    public void SetPlayTimer(float time)
    {
        string timeString = System.TimeSpan.FromSeconds(time).ToString(@"mm\:ss");
        playTimerText.text = timeString;
    }

    public void SetStartWaitTimer(bool isActive)
    {
        startWaitTimerText.gameObject.SetActive(isActive);
    }

    public void AcitvateStartMessage()
    {
        startWaitTimerText.gameObject.SetActive(false);
        transform.Find("START").gameObject.SetActive(true);
    }

    public void SetStartWaitTimerText(float time)
    {
        startWaitTimerText.text = Mathf.Ceil(time).ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        startWaitTimerText = transform.Find("StartTimer").GetComponent<TMPro.TMP_Text>();
        playTimerText = transform.Find("PlayTimer").GetComponent<TMPro.TMP_Text>();

        initNoticeFade = transform.Find("InitNotice").GetComponent<Fade>();
        progress = transform.Find("Image").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        isInitNoticeFaded = initNoticeFade.isFadeOut;

        progress.fillAmount = GameManager.instance.actionProgress;
    }
}
