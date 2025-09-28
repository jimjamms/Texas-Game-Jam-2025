using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { FreeRoam, Bugs, Music, PopUp }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    GameState state;

    private void Start()
    {
        BugTaskController.Instance.OnShowUI += () =>
        {
            state = GameState.Bugs;
        };

        BugTaskController.Instance.OnCloseUI += () =>
        {
            if (state == GameState.Bugs)
                state = GameState.FreeRoam;
        };

        UIManager.Instance.OnShowUI += () =>
        {
            state = GameState.Music;
        };

        UIManager.Instance.OnCloseUI += () =>
        {
            if (state == GameState.Music)
                state = GameState.FreeRoam;
        };

        PopUpController.Instance.OnShowUI += () =>
        {
            state = GameState.PopUp;
            Debug.Log("popup!");
        };

        PopUpController.Instance.OnCloseUI += () =>
        {
            if (state == GameState.PopUp)
                state = GameState.FreeRoam;
        };
    }
    private void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
        }
        else if (state == GameState.Bugs)
        {
            BugTaskController.Instance.HandleUpdate();
        }
    }
}
