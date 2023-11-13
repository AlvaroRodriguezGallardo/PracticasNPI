using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

using Leap;
using Leap.Unity;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;

public class MainController : MonoBehaviour
{
    public enum TrackingOptions
    {
        Desktop,
        Screentop
    }

    [SerializeField]
    public TrackingOptions TrackingMode;



    public GameObject DesktopTracker;
    public GameObject ScreentopTracker;
    public GameObject HandsGameobject;

    LeapProvider leapProvider;

    int handId;
    bool handIdIsValid = false;
    bool wasHandOpen = true;
    public Transform leftHand;
    public Transform rightHand;

    float lastGestureDetectionTime = 0f;
    float gestureCooldown = 0.1f;  // Ajusta según sea necesario

    private string previousScene;
    public MenuController menuController;

    void Start()
    {

        handIdIsValid = false;

        //Seleccionamos el modo de trackeo
        switch (TrackingMode)
        {
            case TrackingOptions.Desktop:
                DesktopTracker.SetActive(true);
                leapProvider = DesktopTracker.GetComponent<LeapProvider>();
                break;
            case TrackingOptions.Screentop:
                ScreentopTracker.SetActive(true);
                leapProvider = ScreentopTracker.GetComponent<LeapProvider>();
                break;
        }
        if (menuController == null)
        {
            Debug.LogError("Asigna el controlador de menú en el Inspector.");
        }

        previousScene = SceneManager.GetActiveScene().name;

        //Cada vez que se detecte un nuevo frame, se llamará a la función OnUpdateFrame
        leapProvider.OnUpdateFrame += OnUpdateFrame;
    }

    // Update is called once per frame
    void OnUpdateFrame(Frame frame)
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            HandsGameobject.SetActive(!HandsGameobject.activeSelf);
        }
        //Comprobamos que se detecte alguna mano
        if(frame.Hands.Count == 0){
            handIdIsValid = false;
        }
        else{

            //Si no teniamos alguna mano guardada de antemano, guardamos una
            if(!handIdIsValid || frame.Hand(handId) == null){
                handId = frame.Hands[0].Id;
                handIdIsValid = true;
            }


            //Obtenemos la mano que queremos trackear
            Hand hand = frame.Hand(handId);

            //Movemos el cursor a la palma de la mano
            Vector3 handWorldPos = hand.PalmPosition;
            Vector2 handScreenPos = Camera.main.WorldToScreenPoint(handWorldPos);
            Mouse.current.WarpCursorPosition(handScreenPos);
            
            
            // Detectar cambio en el estado del puño
            bool isHandOpen = hand.GrabStrength < 0.8f;  // Puedes ajustar este umbral según sea necesario
    
            if (wasHandOpen && !isHandOpen)
            {
                if (DetectGestoPersonalizado(hand))
                {
                    // Solo realizar la acción si el gesto comienza y ha pasado suficiente tiempo desde la última detección
                    if (Time.time - lastGestureDetectionTime > gestureCooldown)
                    {
                        // Realizar la acción de volver al menú anterior
                        menuController.RetrocederAMenuAnterior();

                        // Actualizar el estado de la detección y el tiempo
                        lastGestureDetectionTime = Time.time;
                    }
                }
                else
                {
                    SimularClickIzquierdo();
                }
            }

            

            wasHandOpen = isHandOpen;// Solo realizar acciones cuando se detecta un cambio de estado de abierto a cerrado

            

        }


    }


    public void SimularClickIzquierdo()
    {
        Mouse.current.CopyState<MouseState>(out var mouseState);
        mouseState.WithButton(MouseButton.Left, true);
        InputState.Change( Mouse.current, mouseState);
        //InputSystem.Update();

        Debug.Log("Click!");
    }

    bool DetectGestoPersonalizado(Hand hand)
    {
        // Verifica si el puño está cerrado
        bool isHandClosed = hand.GrabStrength > 0.8f;
        Debug.Log(isHandClosed);

        // Verifica si el pulgar está abierto
        bool isThumbOpen = hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].IsExtended;
        Debug.Log(isThumbOpen);

        // Verifica si el pulgar está apuntando hacia la izquierda
        Vector3 thumbDirection = hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].Bone(Bone.BoneType.TYPE_DISTAL).Direction;
        Debug.Log(thumbDirection);
        bool isThumbPointingLeft = thumbDirection.x < -0.6f;  // Ajusta según sea necesario

        // Combina las condiciones según tus criterios
        return isHandClosed && isThumbOpen && isThumbPointingLeft;
    }


    /*public void VolverAlMenuAnterior()
    {
        // Carga la escena anterior
        SceneManager.LoadScene(previousScene);
    }
    */
    
}
