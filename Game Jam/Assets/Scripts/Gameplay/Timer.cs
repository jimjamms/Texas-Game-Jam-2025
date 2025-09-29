using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Timer : MonoBehaviour, Interactable
{

    public TextMeshProUGUI timerText;
    public GameObject catSprite;
    public GameObject CatBG;
    public TextMeshProUGUI scoreText;
    public float remainingTime = 15;

    public PetCat petCat;
    //public bool catStatus = false;

    public void Interact()
    {
        Debug.Log("meow");
    }

    public int score;

    public event Action OnShowUI;
    public event Action OnCloseUI;

    public static Timer Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // scoreText.text = petCat.getScore().ToString() + " PETS!";
    }


    // Update is called once per frame
    public void HandleUpdate()
    {
        if (remainingTime > 0)
        {
            //catStatus = true;
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            //catStatus = false;
            OnCloseUI?.Invoke();
            catSprite.SetActive(false);
            CatBG.SetActive(false);
            timerText.gameObject.SetActive(false);
            scoreText.gameObject.SetActive(false);
        }

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        // scoreText.text = petCat.getScore().ToString() + " PETS!";
        //Debug.Log("score: " + score);
    }

    // public void AddPoint()
    // {

    // }

}
