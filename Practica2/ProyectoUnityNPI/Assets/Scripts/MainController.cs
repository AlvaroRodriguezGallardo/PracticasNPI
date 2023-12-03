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
    float gestureCooldown = 1.5f;  // Ajusta según sea n
    Vector3 previousHandPosition;
    bool isGestoIniciado = false;
    float gestoStartTime = 0f;


    void Start()
    {

        Screen.SetResolution(2160, 3840, FullScreenMode.FullScreenWindow); 
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
    }

    // Update is called once per frame
    void OnUpdateFrame(Frame frame)
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            HandsGameobject.SetActive(!HandsGameobject.activeSelf);
        }

        //Obtenemos la mano que queremos trackear
        Hand hand = GetCurrentHand(frame);
        //Comprobamos que se detecte alguna mano
        if(hand != null){
        
            //Movemos el cursor a la palma de la mano
            Vector3 handWorldPos = hand.PalmPosition;
            Vector2 handScreenPos = Camera.main.WorldToScreenPoint(handWorldPos);
            Mouse.current.WarpCursorPosition(handScreenPos);
            
            
            // Detectar cambio en el estado del puño
            bool isHandOpen = hand.GrabStrength < 0.95f;  // Puedes ajustar este umbral según sea necesario
    
            if (wasHandOpen && !isHandOpen)
            {
                if (!DetectGestoPersonalizado())
                {                
                    if (Time.time - lastGestureDetectionTime > gestureCooldown)
                    {
                        // Realizar la acción de clicar
                        SimularClickIzquierdo();
                        // Actualizar el estado de la detección y el tiempo
                        lastGestureDetectionTime = Time.time;
                    }
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

    public bool DetectGestoPersonalizado()
    {
        Frame frame = leapProvider.CurrentFrame;
        Hand hand = GetCurrentHand(frame);

        if (hand != null)
        {
            // Verifica si el puño está cerrado
            bool isHandClosed = hand.GrabStrength > 0.95f;

            // Verifica si el pulgar está abierto
            bool isThumbOpen = hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].IsExtended;

            // Verifica si el pulgar está apuntando hacia la izquierda
            Vector3 thumbDirection = hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].Bone(Bone.BoneType.TYPE_DISTAL).Direction;
            bool isThumbPointingLeft = thumbDirection.x < -0.6f;

            // Verifica la transición rápida hacia la izquierda y luego hacia la derecha después de cerrar el puño
            if (isHandClosed && isThumbOpen && isThumbPointingLeft)
            {
                if (!isGestoIniciado)
                {
                    // Si el gesto no ha comenzado, inicia el seguimiento del tiempo y la posición
                    gestoStartTime = Time.time;
                    previousHandPosition = hand.PalmPosition;
                    isGestoIniciado = true;
                }
                else
                {
                    // Si el gesto ha comenzado, verifica la transición rápida y la traslación de la mano
                    float gestoDuration = Time.time - gestoStartTime;
                    Vector3 currentHandPosition = hand.PalmPosition;
                    float translationX = currentHandPosition.x - previousHandPosition.x;

                    Debug.Log("currentHandPosition: " + translationX);
                    Debug.Log("gestoDuration: " + gestoDuration);

                    if (gestoDuration < 0.5f && translationX < -0.05f)  // Ajusta según sea necesario
                    {
                        // Restablece el estado del gesto y devuelve true
                        isGestoIniciado = false;
                        return true;
                    }
                }
            }
            else
            {
                // Si el pulgar no está apuntando a la izquierda, reinicia el estado del gesto
                isGestoIniciado = false;
            }

            return false;
        }
        else return false;
    }

    public bool DetectGestoComedor(){
        Frame frame = leapProvider.CurrentFrame;
        Hand hand = GetCurrentHand(frame);

        if (hand != null)
        {
            // Obtén la dirección del pulgar
            Vector3 thumbDirection = hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].Bone(Bone.BoneType.TYPE_DISTAL).Direction;

            // Obtén la dirección del índice
            Vector3 indexDirection = hand.Fingers[(int)Finger.FingerType.TYPE_INDEX].Bone(Bone.BoneType.TYPE_DISTAL).Direction;

            // Puedes comparar las direcciones según tus criterios, por ejemplo, verificar si la dirección del índice rota hacia la izquierda
            bool isRotationLeft = Vector3.Dot(Vector3.Cross(indexDirection, thumbDirection), hand.PalmNormal) < 0;

            // Comprueba que el pulgar está mirando hacia arriba
            bool thumbIsFacingUp = hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].IsExtended;

            // Puedes ajustar el umbral según sea necesario
            return isRotationLeft && thumbIsFacingUp;
        }
        else
        {
            return false;
        }
    }


    Hand GetCurrentHand(Frame frame){

        //Comprobamos que se detecte alguna mano
        if(frame.Hands.Count == 0){
            handIdIsValid = false;
            return null;
        }
        else{

            //Si no teniamos alguna mano guardada de antemano, guardamos una
            if(!handIdIsValid || frame.Hand(handId) == null){
                handId = frame.Hands[0].Id;
                handIdIsValid = true;
            }

            //Obtenemos la mano que queremos trackear
            return frame.Hand(handId);
        
        }
    
    }



}

