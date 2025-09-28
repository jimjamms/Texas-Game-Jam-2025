using UnityEngine.EventSystems;
using Unity.VisualScripting;
using UnityEngine;

public class PetTheCat
{

    int score = 0;
    public GameObject catSprite;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsMouseOverCat())
        {
            score++;
            Debug.Log("score: " + score);
        }
    }

    private bool IsMouseOverCat()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

}

// what do i want this code to do?
/* 
- press mouse down and IF its over the cat, it'll purr
- ??? (tracking progression) if the cat purrs for 5 seconds, complete task
- text in top left to show how long theyve been petting the cat
*/