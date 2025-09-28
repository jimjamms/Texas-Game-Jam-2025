using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;
public class BugTaskController : MonoBehaviour, Interactable
{
    [SerializeField] PopUpController popUpController;

    // smashing bugs
    [SerializeField] GameObject ui;
    [SerializeField] GameObject bugs;
    [SerializeField] int bugsSmashed;

    public event Action OnShowUI;
    public event Action OnCloseUI;

    // timer
    public float counter = 0;
    private float timeToAct = 1f;
    enum CountingMethod
    {
        Frames,
        Coroutine,
        Invoke
    }
    [SerializeField] private CountingMethod countingMethod;
    public void Interact()
    {
        StartCoroutine(Instance.showUI());
    }

    public static BugTaskController Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    public IEnumerator showUI()
    {
        yield return new WaitForEndOfFrame();

        if (PopUpController.choice == "bugs")
        {
            OnShowUI?.Invoke();
            ui.SetActive(true);

            // bug smash game
            bugsSmashed = 0;
            bugs.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-200, 200), Random.Range(-100, 100));
            bugs.SetActive(true);
        }

    }
    public void smashBugs()
    {
        bugsSmashed++;
        bugs.SetActive(false);

    }

    public void HandleUpdate()
    {
        // bugs game
        if (bugs.activeSelf == false)
        {
            if (countingMethod == CountingMethod.Frames)
            {
                if (counter < timeToAct)
                {
                    counter += Time.deltaTime;
                }
                else
                {
                    counter = 0;
                    bugs.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-200, 200), Random.Range(-100, 100));
                    bugs.SetActive(true);
                }
            }
        }

        // close UI
        if (bugsSmashed == 5)
        {
            ui.SetActive(false);
            OnCloseUI?.Invoke();
        }
    }
}
