using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class PetCat : MonoBehaviour, Interactable
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public float remainingTime = 15;
    public int score = 0;


    [SerializeField] GameObject catUI;
    [SerializeField] GameObject catSprite;
    [SerializeField] GameObject texts;

    //[SerializeField] GameObject catGame;
    public event Action OnShowUI;
    public event Action OnCloseUI;
    public void Interact()
    {
        Debug.Log("meow");
        StartCoroutine(Instance.showUI());
    }

    public static PetCat Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    public IEnumerator showUI()
    {
        yield return new WaitForEndOfFrame();
        Debug.Log("meow");

        OnShowUI?.Invoke();
        catUI.SetActive(true);
        catSprite.SetActive(true);
        texts.SetActive(true);

    }
    public void HandleUpdate()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            //catStatus = false;
            OnCloseUI?.Invoke();
            catUI.SetActive(false);
            catSprite.SetActive(false);
            texts.SetActive(false);
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // scoreText.text = petCat.getScore().ToString() + " PETS!";
        //Debug.Log("score: " + score);
    }
    void Start()
    {
        // catUI.SetActive(true);
        // catSprite.SetActive(true);
        // texts.SetActive(true);
        scoreText.text = score.ToString() + " PETS!";
    }

    void OnMouseDown()
    {
        score++;
        scoreText.text = score.ToString() + " PETS!";
    }

}