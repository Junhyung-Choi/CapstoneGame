using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUIManager : MonoBehaviour
{
    public TutorialChecker tutorial;
    GameObject InitMessage;
    GameObject Ment, Stop, SmallGuide, startUI;
    Slider slider;
    TMPro.TMP_Text scoreText;

    Image progress;

    public float rightStepMaxTime;

    // Start is called before the first frame update
    void Start()
    {
        InitMessage = this.transform.Find("GuideBox").gameObject;
        Ment = this.transform.Find("Guidebox_S").Find("Ment").gameObject;
        SmallGuide = this.transform.Find("Guidebox_S").gameObject;
        Stop = this.transform.Find("Stop").gameObject;
        startUI = this.transform.Find("Steps_8").Find("Start").gameObject;
        slider = this.transform.Find("Slider").GetComponent<Slider>();
        
        scoreText = this.transform.Find("Score").GetComponent<TMPro.TMP_Text>();
        progress = this.transform.Find("Image").GetComponent<Image>();
    }

    public void ShowMent(string ment)
    {
        if(InitMessage.activeSelf) { 
            InitMessage.SetActive(false); 
        }

        if(!SmallGuide.activeSelf) {
            SmallGuide.SetActive(true);
        }

        Ment.SetActive(true);
        Ment.GetComponent<TMPro.TMP_Text>().text = ment;
    }

    public void UpdateScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void UpdateScore(string value)
    {
        scoreText.text = value;
    }

    public void UpdateSlider(float value)
    {
        if(value < 0.5f)
        {
            slider.gameObject.SetActive(false);
            return;
        }
        if(slider.gameObject.activeSelf == false)
        {
            slider.gameObject.SetActive(true);
        }
        if(slider.maxValue == 1)
        {
            slider.maxValue = rightStepMaxTime;
        }
        slider.value = value;
    }

    public void SetActiveStartUI(bool value)
    {
        startUI.SetActive(value);
    }


    private void Update() {
        progress.fillAmount = tutorial.progress;
    }


}
