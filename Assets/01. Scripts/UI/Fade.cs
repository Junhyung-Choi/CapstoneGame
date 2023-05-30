using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public float solidTime = 3f, fadeTime = 1f;
    public bool isFadeOut = false;

    public bool isNotice;
    Coroutine coroutine;
    CanvasGroup canvasGroup;

    private void OnEnable() {
        canvasGroup = GetComponent<CanvasGroup>();
        if(coroutine != null) {
            StopCoroutine(coroutine);
        }
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeIn() {
        canvasGroup.alpha = 0f;
        while (canvasGroup.alpha < 1f) {
            canvasGroup.alpha += Time.deltaTime / fadeTime;
            yield return null;
        }
    }

    IEnumerator FadeOut() {
        canvasGroup.alpha = 1f;
        yield return new WaitForSecondsRealtime(solidTime);
        while (canvasGroup.alpha > 0f) {
            canvasGroup.alpha -= Time.unscaledDeltaTime / fadeTime;
            yield return null;
        }
        isFadeOut = true;

        if(isNotice) {
            this.gameObject.SetActive(false);
        }
    }
}
