using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{

    public TextMeshProUGUI timerText;
    public GameObject catSprite;
    public GameObject CatBG;
    public TextMeshProUGUI scoreText;
    public float remainingTime = 15;

    public PetTheCat petCat;
    //public bool catStatus = false;

    public int score;


    void Start()
    {
        // scoreText.text = petCat.getScore().ToString() + " PETS!";
        Debug.Log("IM PRINTING");
    }


    // Update is called once per frame
    void Update()
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
