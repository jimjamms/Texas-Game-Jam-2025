using Unity.VisualScripting;
using UnityEngine;

public enum GameState { FreeRoam, UI }
public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    GameState state;

    private void Start()
    {
        UIManager.Instance.OnShowUI += () =>
        {
            state = GameState.UI;
        };

        UIManager.Instance.OnCloseUI += () =>
        {
            if (state == GameState.UI)
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
            UIManager.Instance.HandleUpdate();
        }
    }
}
