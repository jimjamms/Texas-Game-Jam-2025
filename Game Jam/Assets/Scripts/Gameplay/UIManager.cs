using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, Interactable
{
    [SerializeField] PopUpController popUpController;

    [Header("Main UI References")]
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject simonSaysGamePanel;

    [Header("Simon Says Buttons (Red, Green, Blue, Yellow)")]
    private Button[] buttons;

    [Header("Game Settings")]
    [SerializeField] private float lightSpeed = 0.5f;
    [SerializeField] private Color inactiveColor = Color.white;

    private int[] lightOrder;
    private int level = 0;
    private int buttonsClicked = 0;
    private int colorOrderRunCount = 0;
    private bool won = false;
    private Color[] baseColors;

    private Coroutine currentSequence;

    public event Action OnShowUI;
    public event Action OnCloseUI;

    public void Interact()
    {
        StartCoroutine(Instance.showUI());
    }

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        // Assign buttons automatically
        buttons = new Button[4];
        buttons[0] = ui.transform.Find("Red").GetComponent<Button>();
        buttons[1] = ui.transform.Find("Green").GetComponent<Button>();
        buttons[2] = ui.transform.Find("Blue").GetComponent<Button>();
        buttons[3] = ui.transform.Find("Yellow").GetComponent<Button>();

        baseColors = new Color[buttons.Length];
        baseColors[0] = Color.red;
        baseColors[1] = Color.green;
        baseColors[2] = Color.blue;
        baseColors[3] = Color.yellow;

        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i;
            buttons[i].onClick.AddListener(() => ButtonClickOrder(index));
        }
    }

    public IEnumerator showUI()
    {
        yield return new WaitForEndOfFrame();

        if (popUpController.choice == "music")
        {
            Debug.Log("meow");
            OnShowUI?.Invoke();
            ui.SetActive(true);
            ResetGame();

            // Generate random sequence
            lightOrder = new int[4];
            for (int i = 0; i < lightOrder.Length; i++)
            {
                lightOrder[i] = UnityEngine.Random.Range(0, buttons.Length);
            }

            level = 1;
            currentSequence = StartCoroutine(PlaySequence());
        }
    }

    private void ResetGame()
    {
        level = 0;
        buttonsClicked = 0;
        colorOrderRunCount = -1;
        won = false;

        foreach (var button in buttons)
            button.GetComponent<Image>().color = inactiveColor;
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
            CloseUI();
    }

    private void CloseUI()
    {
        ui.SetActive(false);
        OnCloseUI?.Invoke();
    }

    public void ButtonClickOrder(int buttonIndex)
    {
        if (won || buttonsClicked >= 4) return;

        buttonsClicked++;

        // Correct button
        if (buttonIndex == lightOrder[buttonsClicked - 1])
        {
            if (buttonsClicked == level)
            {
                if (level < 4)
                {
                    level++;
                    buttonsClicked = 0;
                    if (currentSequence != null) StopCoroutine(currentSequence);
                    currentSequence = StartCoroutine(PlaySequence());
                }
                else
                {
                    // Completed all 4 steps
                    won = true;
                    StartCoroutine(ColorBlinkAll(Color.white));
                }
            }
        }
        else
        {
            // Wrong button → blink red and restart sequence
            if (currentSequence != null) StopCoroutine(currentSequence);
            StartCoroutine(ColorBlinkAll(Color.red));
        }
    }

    private IEnumerator ColorBlinkAll(Color color)
    {
        foreach (var button in buttons)
            button.GetComponent<Image>().color = color;

        yield return new WaitForSeconds(lightSpeed);

        foreach (var button in buttons)
            button.GetComponent<Image>().color = inactiveColor;

        yield return new WaitForSeconds(lightSpeed);

        if (!won)
        {
            buttonsClicked = 0;
            currentSequence = StartCoroutine(PlaySequence());
        }
        else
        {
            CloseUI();
        }
    }

    private IEnumerator PlaySequence()
    {
        buttonsClicked = 0;
        colorOrderRunCount++;

        DisableInteractableButtons();

        for (int i = 0; i < level; i++)
        {
            int index = lightOrder[i];
            Image img = buttons[index].GetComponent<Image>();

            // Flash: show the real color, not gray
            buttons[index].interactable = true;
            yield return new WaitForSeconds(lightSpeed);

            // Return to inactive color
            buttons[index].interactable = false;
            img.color = inactiveColor;
            yield return new WaitForSeconds(lightSpeed);
        }

        EnableInteractableButtons();
    }

    private void DisableInteractableButtons()
    {
        foreach (var button in buttons)
            button.interactable = false;
    }

    private void EnableInteractableButtons()
    {
        foreach (var button in buttons)
            button.interactable = true;
    }
}