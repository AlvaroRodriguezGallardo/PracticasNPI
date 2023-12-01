using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScriptScroll : MonoBehaviour
{
    private MainController mainController;
    [SerializeField]
    public ScrollRect scrollRect;
    public float scrollSpeed = 0.1f; // Ajusta según sea necesario
    void Start()
    {
        mainController = FindObjectOfType<MainController>();
    }
    // Update is called once per frame
    void Update()
    {

        if(mainController != null && mainController.DetectGestoScroll()){
            Scroll();
        }
    }
    public void Scroll()
    {
        scrollRect.verticalNormalizedPosition -= scrollSpeed;
        Debug.Log("Scrolleando");
    }
}
