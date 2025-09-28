using Unity.Multiplayer.Center.Common.Analytics;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { FreeRoam, UI, PopUp }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    GameState state;

    private void Start()
    {
        BugTaskController.Instance.OnShowUI += () =>
        {
            state = GameState.UI;
        };

        BugTaskController.Instance.OnCloseUI += () =>
        {
            if (state == GameState.UI)
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
        else if (state == GameState.UI)
        {
            BugTaskController.Instance.HandleUpdate();
        }
    }
}
