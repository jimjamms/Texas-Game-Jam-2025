using UnityEngine;
using System;
using System.Collections;
using TMPro;
using Random = UnityEngine.Random;
using System.Collections.Generic;

public class PopUpController : MonoBehaviour
{
    public List<string> tasks;
    public int randomIndex;
    public static string choice;

    // checks other UIs aren't opened
    [SerializeField] GameObject bugUI;
    [SerializeField] GameObject musicUI;
    [SerializeField] GameObject artUI;

    // pop up
    [SerializeField] GameObject popupUI;
    [SerializeField] TMP_Text textUI;

    // hour text
    public TMP_Text hourText;
    public int timeLeft;

    public event Action OnShowUI;
    public event Action OnCloseUI;

    // timer
    public float counter = 0;
    private float timeToAct = 5f;
    enum CountingMethod
    {
        Frames,
        Coroutine,
        Invoke
    }
    [SerializeField] private CountingMethod countingMethod;

    public static PopUpController Instance { get; private set; }
    void Awake()
    {
        tasks = new List<string> { "bugs", "music", "art" };
        Instance = this;
        timeLeft = 24;
        hourText.text = "" + timeLeft;
    }

    public IEnumerator showUI()
    {
        yield return new WaitForEndOfFrame();

        OnShowUI?.Invoke();
        popupUI.SetActive(true);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        showUI();
        // popup
        if (countingMethod == CountingMethod.Frames)
        {
            if (counter < timeToAct)
            {
                counter += Time.deltaTime;
            }
            else
            {
                choice = "";
                counter = 0;
                if (popupUI.activeSelf == false && bugUI.activeSelf == false && musicUI.activeSelf == false && artUI.activeSelf == false)
                {
                    popupUI.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-200, 200), Random.Range(-100, 100));
                    randomIndex = Random.Range(0, tasks.Count);
                    Debug.Log(randomIndex);
                    textUI.text = tasks[randomIndex];
                    popupUI.SetActive(true);
                }
            }
        }
    }

    public void reject()
    {
        counter = 0;
        popupUI.SetActive(false);
        OnCloseUI?.Invoke();
    }

    public void accept()
    {
        counter = 0;
        timeLeft--;
        choice = tasks[randomIndex];
        hourText.text = "" + timeLeft;
        popupUI.SetActive(false);
        OnCloseUI?.Invoke();
    }
}
