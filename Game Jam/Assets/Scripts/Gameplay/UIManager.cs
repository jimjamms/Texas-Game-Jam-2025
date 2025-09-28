using Unity.VisualScripting;
using UnityEngine;
using System;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using System.Collections;
using Random = UnityEngine.Random;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject ui;
    [SerializeField] GameObject bugs;

    public event Action OnShowUI;
    public event Action OnCloseUI;

    [SerializeField] int bugsSmashed;

    public float counter = 0;
    private float timeToAct = 1f;

    enum CountingMethod
    {
        Frames,
        Coroutine,
        Invoke
    }
    [SerializeField] private CountingMethod countingMethod;

    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }



    public IEnumerator showUI()
    {
        yield return new WaitForEndOfFrame();

        OnShowUI?.Invoke();
        ui.SetActive(true);

        bugsSmashed = 0;
        bugs.GetComponent<RectTransform>().anchoredPosition = new Vector2(Random.Range(-200, 200), Random.Range(-100, 100));
        bugs.SetActive(true);

    }

    public void smashBugs()
    {
        bugsSmashed++;
        bugs.SetActive(false);

    }

    public void HandleUpdate()
    {
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
        // smashBugs();
        // close UI
        if (bugsSmashed == 5)
        {
            ui.SetActive(false);
            OnCloseUI?.Invoke();
        }
    }
}
