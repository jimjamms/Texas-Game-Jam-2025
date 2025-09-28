using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SceneFade : MonoBehaviour
{
    private Image sceneFadeImage;

    private void Awake()
    {
        sceneFadeImage = GetComponent<Image>();
    }

    public IEnumerator FadeInCoroutine(float duration)
    {
        Color start = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 1);
        Color target = new(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);
        yield return FadeCoroutine(start, target, duration);
        gameObject.SetActive(false);
    }
    public IEnumerator FadeOutCoroutine(float duration)
    {
        Color start = new Color(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);
        Color target = new(sceneFadeImage.color.r, sceneFadeImage.color.g, sceneFadeImage.color.b, 0);
        gameObject.SetActive(true);
        yield return FadeCoroutine(start, target, duration);
    }
    private IEnumerator FadeCoroutine(Color start, Color target, float duration)
    {
        float elapsedTime = 0;
        float elapsedPercent = 0;
        while (elapsedPercent < 1)
        {
            elapsedPercent = elapsedTime / duration;
            sceneFadeImage.color = Color.Lerp(start, target, elapsedPercent);
            yield return null;
            elapsedTime += Time.deltaTime;
        }
    }
}
