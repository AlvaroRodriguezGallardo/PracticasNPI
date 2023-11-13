using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

using Leap;
using Leap.Unity;
using UnityEngine.InputSystem.UI;

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
    bool prevFistState = false;
    bool wasHandOpen = true;
    public Transform leftHand;
    public Transform rightHand;
    //public float intersectionThreshold = 0.1f;


    //private X_Gesture xGesture;

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

        //Cada vez que se detecte un nuevo frame, se llamará a la función OnUpdateFrame
        leapProvider.OnUpdateFrame += OnUpdateFrame;
        //xGesture = new X_Gesture(this);
    }

    // Update is called once per frame
    void Update()
    {
        
        //Muestra y esconde las manos al pulsar la tecla espacio
        if(Input.GetKeyDown(KeyCode.Space)){
            HandsGameobject.SetActive(!HandsGameobject.activeSelf);
        }

        //Para testeo
        /*
        if(handIdIsValid){
            Hand hand = leapProvider.CurrentFrame.Hand(handId);
            if(hand.GrabStrength == 1.0f){
                SimularClickIzquierdo();
            }
        }
        */

    }

    void OnUpdateFrame(Frame frame)
    {
        
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
            /*
            if (xGesture.DetectXGesture(frame))
            {
                Vector3 intersectionPoint = xGesture.CalculateIntersectionPoint(leftHand.position, rightHand.position);
                if (xGesture.IsNearWristIntersection(leftHand.position, rightHand.position, intersectionPoint))
                {
                    // Ejecuta la acción de "Volver a la pestaña anterior"
                    Debug.Log("Volver a la pestaña anterior");
                }
            }
            */

            //Obtenemos la mano que queremos trackear
            Hand hand = frame.Hand(handId);

            //Movemos el cursor a la palma de la mano
            Vector3 handWorldPos = hand.PalmPosition;
            Vector2 handScreenPos = Camera.main.WorldToScreenPoint(handWorldPos);
            Mouse.current.WarpCursorPosition(handScreenPos);
            
            
            // Detectar cambio en el estado del puño
            bool isHandOpen = hand.GrabStrength < 0.05f;  // Puedes ajustar este umbral según sea necesario
            Debug.Log(isHandOpen);
    
            if (wasHandOpen && !isHandOpen)
            {
                SimularClickIzquierdo();
            }

            wasHandOpen = isHandOpen;// Solo realizar acciones cuando se detecta un cambio de estado de abierto a cerrado
            Debug.Log(wasHandOpen);
            
            //Debug.Log(handWorldPos.ToString() + handScreenPos.ToString());

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
    /*
    public Frame GetLeapFrame()
    {
        // Obtén el frame actual de Ultraleap
        return leapProvider.CurrentFrame;
    }
    */
}
