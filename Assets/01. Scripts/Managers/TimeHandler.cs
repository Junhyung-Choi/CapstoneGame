using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeHandler : MonoBehaviour
{
    public static TimeHandler instance;
    
    float timeFactor;
    float maxTimeFactor = 1.0f;
    float minTimeFactor = 0.3f;

    Coroutine coroutine;

    private void Awake() {
        instance = this;
    }
    
    public void SetTimeFactor(float timeFactor)
    {
        if(coroutine != null)
            StopCoroutine(coroutine);

        coroutine = StartCoroutine(_SetTimeFactorCoroutine(timeFactor));
    }

    IEnumerator _SetTimeFactorCoroutine(float timeFactor)
    {
        while(Time.timeScale != timeFactor)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, timeFactor, 0.05f);
            Debug.Log(Time.timeScale);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
