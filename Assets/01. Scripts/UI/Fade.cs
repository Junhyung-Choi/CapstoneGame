using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public float solidTime = 3f, fadeTime = 1f;
    public bool isFadeOut = false;
    CanvasGroup canvasGroup;

    private void OnEnable() {
        canvasGroup = GetComponent<CanvasGroup>();
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
        yield return new WaitForSeconds(solidTime);
        canvasGroup.alpha = 1f;
        while (canvasGroup.alpha > 0f) {
            canvasGroup.alpha -= Time.deltaTime / fadeTime;
            yield return null;
        }
        isFadeOut = true;
    }
}
