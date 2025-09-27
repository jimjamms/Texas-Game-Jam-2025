using Unity.VisualScripting;
using UnityEngine;
using System;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject ui;

    public event Action OnShowUI;
    public event Action OnCloseUI;

    public static UIManager Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }



    public IEnumerator showUI()
    {
        yield return new WaitForEndOfFrame();

        OnShowUI?.Invoke();
        ui.SetActive(true);
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ui.SetActive(false);
            OnCloseUI?.Invoke();
        }
    }
}
