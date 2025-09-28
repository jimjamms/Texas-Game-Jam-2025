using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class draw_tutorial : MonoBehaviour
{
    public Camera m_camera;
    public GameObject brush;

    LineRenderer currentLineRenderer;

    Vector2 currentMousePosition = Input.mousePosition;

    Vector2 lastPos;

    int strokes = 0;
    [SerializeField] private GameObject overlay;
    [SerializeField] private GameObject screen;
    [SerializeField] private GameObject brushObject;

    //[SerializeField] private GameObject brushtexture;

    //var enemy : GameObject;


    [System.Obsolete]
    public void Update()
    {
        float mouseX = currentMousePosition.x;
        float mouseY = currentMousePosition.y;
        //GameObject[] clones = GameObject.FindGameObjectWithTag("clonetag");

        if (Input.GetMouseButtonDown(0) && currentMousePosition != lastPos)
        {
            strokes++;
            Debug.Log(strokes);
        }
        if (strokes >= 6)
        {
            //enemy = GameObject.Find("enemy");

            //Debug.Log("reached end!");
            overlay.SetActive(false);
            screen.SetActive(false);
            brushObject.SetActive(false);

            DestroyAllClones();
            //return;
        }

    }
    //public class DestroyClonesByName : MonoBehaviour
    //{
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
    //}

    void OnMouseOver()
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

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush);
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
