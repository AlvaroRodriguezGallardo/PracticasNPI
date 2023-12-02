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
    float lastGestureDetectionTime = 0f;
    float gestureCooldown = 0.25f;  // Ajusta según sea necesario
    void Start()
    {
        mainController = FindObjectOfType<MainController>();
    }
    // Update is called once per frame
    void Update()
    {

        if(mainController != null && mainController.DetectGestoScrollArriba()){
            if (Time.time - lastGestureDetectionTime > gestureCooldown)
            {
                 ScrollArriba();
                // Realizar la acción de volver al menú anterior
                //accionesGestoComedor.Invoke();
                // Actualizar el estado de la detección y el tiempo
                lastGestureDetectionTime = Time.time;
            }
        
        }
        if(mainController != null && mainController.DetectGestoScrollAbajo()){
            if (Time.time - lastGestureDetectionTime > gestureCooldown)
            {
                 ScrollAbajo();
                // Realizar la acción de volver al menú anterior
                //accionesGestoComedor.Invoke();
                // Actualizar el estado de la detección y el tiempo
                lastGestureDetectionTime = Time.time;
            }
        
        }
    }
    public void ScrollAbajo()
    {
        scrollRect.verticalNormalizedPosition -= scrollSpeed;
        Debug.Log("Scrolleando abajo");
    }
    public void ScrollArriba()
    {
        scrollRect.verticalNormalizedPosition += scrollSpeed;
        Debug.Log("Scrolleando arriba");
    }

}
