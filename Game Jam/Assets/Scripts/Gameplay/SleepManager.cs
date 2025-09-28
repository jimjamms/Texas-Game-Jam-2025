using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SleepManager : MonoBehaviour
{
    [Header("Main UI References")]
    [SerializeField] private GameObject ui;
    [SerializeField] private float sceneFadeDuration;
    private SceneFade sceneFade;
    public event Action OnShowUI;
    public event Action OnCloseUI;
    public Image fadeImage; // Assign your FadePanel Image here
    public CanvasGroup canvasGroup; // Assign your FadePanel's Canvas Group here
    public float fadeDuration = 1.0f;

    public static SleepManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        sceneFade = GetComponentInChildren<SceneFade>();
    }

    private IEnumerator Start()
    {
        yield return sceneFade.FadeInCoroutine(sceneFadeDuration);
    }

    public IEnumerator showUI()
    {
        yield return new WaitForEndOfFrame();
        OnShowUI?.Invoke();
        ui.SetActive(true);

    }

    private void CloseUI()
    {
        ui.SetActive(false);
        OnCloseUI?.Invoke();
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return sceneFade.FadeOutCoroutine(sceneFadeDuration);
        yield return SceneManager.LoadSceneAsync(1);
    }

    public void FadeToBlack()
    {
        StartCoroutine(FadeRoutine(0f, 1f)); // Fade from current alpha to opaque black
    }

    public void FadeFromBlack()
    {
        StartCoroutine(FadeRoutine(1f, 0f)); // Fade from opaque black to transparent
    }

    IEnumerator FadeRoutine(float startAlpha, float targetAlpha)
    {
        float timer = 0f;
        canvasGroup.alpha = startAlpha; // Ensure starting alpha is set

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, timer / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = targetAlpha; // Ensure final alpha is set precisely
    }
    
}