using UnityEngine;
using System;

public class DrawingPen : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;
    Vector2 currentMousePosition;
    LineRenderer currentLineRenderer;
    Vector2 lastPos;
    public int strokes = 0;
    public event Action OnCloseUI;

    public static DrawingPen Instance { get; private set; }
    void Awake()
    {
        //currentMousePosition = Input.mousePosition;
        Instance = this;
    }
    public void OnMouseOver()
    {
        Draw();
    }

    [System.Obsolete]
    public void HandleUpdate()
    { //re
      // turn;
    }

    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject brushObject;

    public string BrushSample;


    [System.Obsolete]
    public void DestroyAllClones()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.name.Contains(BrushSample + "(Clone)"))
            {
                Destroy(obj);
            }
        }
    }

    [System.Obsolete]
    public void Draw()
    {
        float mouseX = currentMousePosition.x;
        float mouseY = currentMousePosition.y;

        //GameObject[] clones = GameObject.FindGameObjectWithTag("clonetag");

        if (Input.GetMouseButtonDown(0) && currentMousePosition != lastPos)
        {
            //strokes++;
            Debug.Log(strokes);
        }

        // close UI
        if (strokes >= 6)
        {
            //enemy = GameObject.Find("enemy");

            //Debug.Log("reached end!");
            //DestroyAllClones();
            overlay.SetActive(false);
            screen.SetActive(false);
            brushObject.SetActive(false);
            strokes = 0;

            DestroyAllClones();
            //return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Debug.Log("inside bounds");
            strokes++;
            CreateBrush();
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
            if (mousePos != lastPos)
            {
                AddAPoint(mousePos);
                lastPos = mousePos;
            }
        }
        else
        {
            currentLineRenderer = null;
        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brushObject);
        //brushInstance.tag = "clonetag";
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();

        Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);

        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
    }

    void AddAPoint(Vector2 pointPos)
    {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

}
