using UnityEngine;

public class BetweenScenes : MonoBehaviour
{
    [SerializeField] public string choice;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        choice = PopUpController.choice;
        Debug.Log(choice);
    }
}
