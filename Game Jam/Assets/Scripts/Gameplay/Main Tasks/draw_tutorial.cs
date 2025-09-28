using UnityEngine;
using System;
using System.Collections;
using UnityEngine.Rendering;

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


            //return;
        }
    }

    public string BrushSample;


    // [System.Obsolete]
    // public void DestroyAllClones()
    // {
    //     GameObject[] allObjects = FindObjectsOfType<GameObject>();
    //     foreach (GameObject obj in allObjects)
    //     {
    //         if (obj.name.Contains(BrushSample + "(Clone)"))
    //         {
    //             Destroy(obj);
    //         }
    //     }
    // }

    // [System.Obsolete]
    // public void HandleUpdate()
    // {
    //     float mouseX = currentMousePosition.x;
    //     float mouseY = currentMousePosition.y;
    //     //GameObject[] clones = GameObject.FindGameObjectWithTag("clonetag");

    //     if (Input.GetMouseButtonDown(0) && currentMousePosition != lastPos)
    //     {
    //         strokes++;
    //         Debug.Log(strokes);
    //     }

    //     // close UI
    //     if (strokes >= 6)
    //     {
    //         //enemy = GameObject.Find("enemy");

    //         //Debug.Log("reached end!");
    //         overlay.SetActive(false);
    //         screen.SetActive(false);
    //         brushObject.SetActive(false);
    //         OnCloseUI?.Invoke();

    //         DestroyAllClones();
    //         //return;
    //     }
    // }

    [SerializeField] public GameObject overlay;
    [SerializeField] public GameObject screen;
    [SerializeField] public GameObject brushObject;

    //[SerializeField] private GameObject brushtexture;

    //var enemy : GameObject;

    // public void Update()
    // {
    //     float mouseX = currentMousePosition.x;
    //     float mouseY = currentMousePosition.y;
    //     //GameObject[] clones = GameObject.FindGameObjectWithTag("clonetag");

    //     if (Input.GetMouseButtonDown(0) && currentMousePosition != lastPos)
    //     {
    //         strokes++;
    //         Debug.Log(strokes);
    //     }
    //     if (strokes >= 6)
    //     {
    //         //enemy = GameObject.Find("enemy");

    //         //Debug.Log("reached end!");
    //         overlay.SetActive(false);
    //         screen.SetActive(false);
    //         brushObject.SetActive(false);

    //         DestroyAllClones();
    //         //return;
    //     }

    // }
    //public class DestroyClonesByName : MonoBehaviour
    //{
    // public string BrushSample;


    // [System.Obsolete]
    // public void DestroyAllClones()
    // {
    //     GameObject[] allObjects = FindObjectsOfType<GameObject>();
    //     foreach (GameObject obj in allObjects)
    //     {
    //         if (obj.name.Contains(BrushSample + "(Clone)"))
    //         {
    //             Destroy(obj);
    //         }
    //     }
    // }
    //}

    // public void Draw()
    // {
    //     float mouseX = currentMousePosition.x;
    //     float mouseY = currentMousePosition.y;

    //     if (Input.GetKeyDown(KeyCode.Mouse0) && mouseX > -2 && mouseX < 4 && mouseY > -2 && mouseY < 2.5)
    //     {
    //         Debug.Log("inside bounds");
    //         CreateBrush();
    //     }
    //     if (Input.GetKey(KeyCode.Mouse0))
    //     {
    //         Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
    //         if (mousePos != lastPos)
    //         {
    //             AddAPoint(mousePos);
    //             lastPos = mousePos;
    //         }
    //     }
    //     else
    //     {
    //         currentLineRenderer = null;
    //     }
    // }

    // void CreateBrush()
    // {
    //     GameObject brushInstance = Instantiate(brush);
    //     //brushInstance.tag = "clonetag";
    //     currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

    //     Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

    //     currentLineRenderer.SetPosition(0, mousePos);
    //     currentLineRenderer.SetPosition(1, mousePos);
    // }

    // void AddAPoint(Vector2 pointPos)
    // {
    //     currentLineRenderer.positionCount++;
    //     int positionIndex = currentLineRenderer.positionCount - 1;
    //     currentLineRenderer.SetPosition(positionIndex, pointPos);
    // }

}
