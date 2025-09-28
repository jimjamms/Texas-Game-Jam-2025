using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;

//----------------------------------------------------

public class PetCat : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score = 0;

    [SerializeField] private GameObject catSprite;
    [SerializeField] private GameObject catBG;
    [SerializeField] private Timer timer;

    void Start()
    {
       scoreText.text = score.ToString() + " PETS!";
    }

    void OnMouseDown()
    {
        score++;
        scoreText.text = score.ToString() + " PETS!";
    }

}