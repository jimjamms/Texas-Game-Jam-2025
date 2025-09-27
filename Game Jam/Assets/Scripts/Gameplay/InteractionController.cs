using UnityEngine;

public class InteractionController : MonoBehaviour, Interactable
{
    public void Interact()
    {
        StartCoroutine(UIManager.Instance.showUI());
    }
}
