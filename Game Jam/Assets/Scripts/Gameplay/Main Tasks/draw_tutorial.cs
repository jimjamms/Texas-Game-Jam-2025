using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class draw_tutorial : MonoBehaviour, Interactable
{
    public Camera m_camera;
    public GameObject brush;

    LineRenderer currentLineRenderer;

    //Vector2 currentMousePosition;

    Vector2 lastPos;

    int strokes = 0;

    [SerializeField] DrawingPen drawing;
    [SerializeField] PopUpController popUpController;

    public event Action OnShowUI;
    public event Action OnCloseUI;

    public void Interact()
    {
        StartCoroutine(Instance.showUI());
    }

    public static draw_tutorial Instance { get; private set; }
    void Awake()
    {
        //currentMousePosition = Input.mousePosition;
        Instance = this;
    }

    public IEnumerator showUI()
    {
        yield return new WaitForEndOfFrame();

        if (PopUpController.choice == "art")
        {
            strokes = 0;
            OnShowUI?.Invoke();
            overlay.SetActive(true);
            screen.SetActive(true);
            brushObject.SetActive(true);
        }

    }

    Vector2 currentMousePosition;

    public void HandleUpdate()
    {
        //drawing.OnMouseOver();

        // close UI
        if (overlay.activeSelf == false)
        {
            OnCloseUI?.Invoke();
            if (PopUpController.timeLeft == 0)
                SceneManager.LoadSceneAsync(4);
        }
    }

    public string BrushSample;

    [SerializeField] public GameObject overlay;
    [SerializeField] public GameObject screen;
    [SerializeField] public GameObject brushObject;

}
