using UnityEngine;

public class InteractionController : MonoBehaviour, Interactable
{
    public void Interact()
    {
        StartCoroutine(SleepManager.Instance.showUI());
    }
}
