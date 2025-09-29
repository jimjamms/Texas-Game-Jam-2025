using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // if you're using TextMeshPro

public class FadeImageOnKey : MonoBehaviour
{
    public Image imageToFade;
    public CanvasGroup canvasGroup;
    public float fadeDuration = 1.0f;
    public GameObject dialogueBox;        // assign in inspector
    public TextMeshProUGUI dialogueText;  // assign in inspector

    private void Start()
    {
        if (imageToFade == null)
            imageToFade = GetComponent<Image>();

        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        // 🔹 Start fully transparent
        SetAlpha(0f);

        // 🔹 Hide dialogue initially
        if (dialogueBox != null)
            dialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            PopUpController.timeLeft -= 4;
            StartCoroutine(FadeSequence());
        }
    }

    private IEnumerator FadeSequence()
    {
        // Step 1: Fade in to black
        yield return StartCoroutine(Fade(1f));

        // Step 2: Wait 5 seconds
        yield return new WaitForSeconds(5f);

        // Step 3: Show dialogue box + text
        if (dialogueBox != null && dialogueText != null)
        {
            dialogueBox.SetActive(true);
            yield return new WaitForSeconds(5f);
        }
        // Step 4: Instantly clear screen
        SetAlpha(0f);
    }

    private IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = GetAlpha();

        for (float t = 0; t < 1; t += Time.deltaTime / fadeDuration)
        {
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, t);
            SetAlpha(newAlpha);
            yield return null;
        }

        SetAlpha(targetAlpha);
    }

    private void SetAlpha(float alpha)
    {
        if (canvasGroup != null)
            canvasGroup.alpha = alpha;
        else
        {
            Color c = imageToFade.color;
            c.a = alpha;
            imageToFade.color = c;
        }
    }

    private float GetAlpha()
    {
        if (canvasGroup != null)
            return canvasGroup.alpha;
        return imageToFade.color.a;
    }
}