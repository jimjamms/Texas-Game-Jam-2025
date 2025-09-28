using UnityEngine;
using TMPro;

public class BetweenScenes : MonoBehaviour
{
    [SerializeField] public string choice;
    [SerializeField] public int timeLeft;
    [SerializeField] public bool dialogueDone;

    // hour text
    public TMP_Text hourText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        choice = PopUpController.choice;
        timeLeft = PopUpController.timeLeft;
        dialogueDone = PopUpController.dialogueDone;
        hourText.text = "" + timeLeft;
        Debug.Log(dialogueDone);
    }
}
