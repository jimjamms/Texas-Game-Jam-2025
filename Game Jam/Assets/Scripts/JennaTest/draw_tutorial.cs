using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class draw_tutorial : MonoBehaviour
{
    // variables
    public Camera m_camera;
    public GameObject brush;

    LineRenderer currentLineRenderer;

    Vector2 currentMousePosition = Input.mousePosition;

    Vector2 lastPos;

    int strokes = 0;
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject brushObject;



    [System.Obsolete]
    public void Update() //tracks if mouse has been pressed and painted
    {
        if (Input.GetMouseButtonDown(0) && currentMousePosition != lastPos)
        {
            strokes++;
            Debug.Log(strokes);
        }
        if (strokes >= 6)
        {
            overlay.SetActive(false);
            screen.SetActive(false);
            brushObject.SetActive(false);
            DestroyAllClones();
        }
    }
    
    public string BrushSample;

    [System.Obsolete]
    public void DestroyAllClones() //remove paint stroke clones
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

    void OnMouseOver() //draw only when mouse is over the draw screen
    {
        Draw();
    }

    void Draw()
    {
        float mouseX = currentMousePosition.x;
        float mouseY = currentMousePosition.y;

        if (Input.GetKeyDown(KeyCode.Mouse0) && mouseX > -2 && mouseX < 4 && mouseY > -2 && mouseY < 2.5)
        {
            Debug.Log("inside bounds");
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

    void CreateBrush() //creates new brush instance/clone
    {
        GameObject brushInstance = Instantiate(brush);
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
