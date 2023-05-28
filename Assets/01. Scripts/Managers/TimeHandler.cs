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
        float finalTimeFactor = timeFactor * 2.0f;
        while(Time.timeScale != finalTimeFactor)
        {
            if(GameManager.isPaused) break;
            Time.timeScale = Mathf.Lerp(Time.timeScale, finalTimeFactor, 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
