using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

using Leap;
using Leap.Unity;
using UnityEngine.InputSystem.UI;
using UnityEngine.InputSystem.Controls;
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
    public Vector3 previousHandPosition;
    bool isGestoIniciado = false;
    float gestoStartTime = 0f;

    public float TiempoLimiteInactividad;
    public GameObject Tutorial, MainMenu;
    public List<GameObject> OtrosMenus;

    float tiempoInactividad = 0;
    Vector3 lastMousePos;
    bool desactivarTutorialConMovimiento = true;
    bool areAllFingersExtended = false;
    float fingerCheckCooldown = 0.5f; // Tiempo de espera antes de cambiar areAllFingersExtended
    float fingerCheckTimer = 0f;

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

        lastMousePos = Input.mousePosition;
    }

    void Update()
    {

        //Todo esto sirve para activar el tutorial tras un tiempo de inactividad,
        //y para desactivarlo cuando se detecte movimiento
        if (Input.mousePosition != lastMousePos)
        {
            tiempoInactividad = 0;
            if (desactivarTutorialConMovimiento)
                Tutorial.SetActive(false);
        }
        else
        {
            tiempoInactividad += Time.deltaTime;
        }

        if (tiempoInactividad >= TiempoLimiteInactividad)
        {
            Tutorial.SetActive(true);
            MainMenu.SetActive(true);

            foreach (GameObject menu in OtrosMenus)
                menu.SetActive(false);

        }

        lastMousePos = Input.mousePosition;
        //Debug.Log(tiempoInactividad);


    }

    // Update is called once per frame
    void OnUpdateFrame(Frame frame)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            HandsGameobject.SetActive(!HandsGameobject.activeSelf);
        }

        //Obtenemos la mano que queremos trackear
        Hand hand = GetCurrentHand(frame);
        //Comprobamos que se detecte alguna mano
        if (hand != null)
        {

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
        InputState.Change(Mouse.current, mouseState);
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
        else return false;
    }

    public bool DetectGestoComedor()
    {
        Frame frame = leapProvider.CurrentFrame;
        Hand hand = GetCurrentHand(frame);

        if (hand != null && !hand.IsLeft)
        {
            // Verifica que el pulgar esté extendido y apuntando hacia arriba
            bool thumbIsFacingUp = hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].IsExtended && hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].Bone(Bone.BoneType.TYPE_DISTAL).Direction.y > 0.5f;

            // Verifica que los otros cuatro dedos estén extendidos
            bool fingersAreExtended = true;
            for (int i = 1; i < 5; i++)
            {
                fingersAreExtended &= hand.Fingers[i].IsExtended;
            }

            // Obtén la dirección de los dedos índice, medio, anular y meñique
            Vector3[] fingerDirections = new Vector3[4];
            for (int i = 1; i < 5; i++)
            {
                fingerDirections[i - 1] = hand.Fingers[i].Bone(Bone.BoneType.TYPE_DISTAL).Direction.normalized;
            }

            // Puedes ajustar el umbral según sea necesario
            bool fingersAreRotated = true;
            for (int i = 0; i < 4; i++)
            {
                // Verifica que la dirección del dedo rota hacia el centro de la palma
                fingersAreRotated &= Vector3.Dot(fingerDirections[i], hand.PalmNormal) > 0.5f;
            }

            // Combinar todas las condiciones según tus criterios
            return thumbIsFacingUp && fingersAreExtended && fingersAreRotated;
        }
        else
        {
            return false;
        }
    }

    public bool DetectGestoComedorIzquierda()
    {
        Frame frame = leapProvider.CurrentFrame;
        Hand hand = GetCurrentHand(frame);

        if (hand != null && hand.IsLeft)
        {
            // Verifica que el pulgar esté extendido y apuntando hacia arriba
            bool thumbIsFacingUp = hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].IsExtended && hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].Bone(Bone.BoneType.TYPE_DISTAL).Direction.y > 0.5f;

            // Verifica que los otros cuatro dedos estén extendidos
            bool fingersAreExtended = true;
            for (int i = 1; i < 5; i++)
            {
                fingersAreExtended &= hand.Fingers[i].IsExtended;
            }

            // Obtén la dirección de los dedos índice, medio, anular y meñique
            Vector3[] fingerDirections = new Vector3[4];
            for (int i = 1; i < 5; i++)
            {
                fingerDirections[i - 1] = hand.Fingers[i].Bone(Bone.BoneType.TYPE_DISTAL).Direction.normalized;
            }

            // Puedes ajustar el umbral según sea necesario
            bool fingersAreRotated = true;
            for (int i = 0; i < 4; i++)
            {
                // Verifica que la dirección del dedo rota hacia el centro de la palma (para la mano izquierda, cambia la condición a < 0.5f)
                fingersAreRotated &= Vector3.Dot(fingerDirections[i], hand.PalmNormal) > 0.5f;
            }

            // Combinar todas las condiciones según tus criterios
            return thumbIsFacingUp && fingersAreExtended && fingersAreRotated;
        }
        else
        {
            return false;
        }
    }

public bool DetectGestoScrollArriba()
{
    Frame frame = leapProvider.CurrentFrame;
    Hand hand = GetCurrentHand(frame);

    if (hand != null)
    {
        // Verifica que los dedos pulgar, anular y meñique estén cerrados
        bool areThumbRingPinkyClosed = !hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].IsExtended
            && !hand.Fingers[(int)Finger.FingerType.TYPE_RING].IsExtended
            && !hand.Fingers[(int)Finger.FingerType.TYPE_PINKY].IsExtended;

        // Verifica que los dedos índice y corazón estén extendidos
        bool areIndexMiddleExtended = hand.Fingers[(int)Finger.FingerType.TYPE_INDEX].IsExtended
            && hand.Fingers[(int)Finger.FingerType.TYPE_MIDDLE].IsExtended;

        // Verifica el ángulo entre los dedos y la palma de la mano
        float angleThresholdIndex = 85.0f;

        // Calcula el ángulo entre los dedos y la dirección de la palma
        float indexAngle = Vector3.Angle(hand.Fingers[(int)Finger.FingerType.TYPE_INDEX].Direction, hand.PalmNormal);
        float middleAngle = Vector3.Angle(hand.Fingers[(int)Finger.FingerType.TYPE_MIDDLE].Direction, hand.PalmNormal);

        // Verifica si el ángulo entre los dedos y la palma es mayor que el umbral
        bool isAngleGreaterThanThreshold = indexAngle > angleThresholdIndex;

        // Si todos los dedos están extendidos, hay un movimiento hacia abajo y el ángulo es mayor que el umbral
        if (areThumbRingPinkyClosed && areIndexMiddleExtended && isAngleGreaterThanThreshold)
        {
            return true;
        }
    }

    return false;
}

public bool DetectGestoScrollAbajo()
{
    Frame frame = leapProvider.CurrentFrame;
    Hand hand = GetCurrentHand(frame);

    if (hand != null)
    {
        // Verifica que los dedos pulgar, anular y meñique estén cerrados
        bool areThumbRingPinkyClosed = !hand.Fingers[(int)Finger.FingerType.TYPE_THUMB].IsExtended
            && !hand.Fingers[(int)Finger.FingerType.TYPE_RING].IsExtended
            && !hand.Fingers[(int)Finger.FingerType.TYPE_PINKY].IsExtended;

        // Verifica que los dedos índice y corazón estén extendidos
        bool areIndexMiddleExtended = hand.Fingers[(int)Finger.FingerType.TYPE_INDEX].IsExtended
            && hand.Fingers[(int)Finger.FingerType.TYPE_MIDDLE].IsExtended;

        // Verifica el ángulo entre los dedos y la palma de la mano
        float angleThresholdIndex = 60f;
        // Calcula el ángulo entre los dedos y la dirección de la palma
        float indexAngle = Vector3.Angle(hand.Fingers[(int)Finger.FingerType.TYPE_INDEX].Direction, hand.PalmNormal);
        float middleAngle = Vector3.Angle(hand.Fingers[(int)Finger.FingerType.TYPE_MIDDLE].Direction, hand.PalmNormal);

        Debug.Log("indexAngle: " + indexAngle);
        // Verifica si el ángulo entre los dedos y la palma es mayor que el umbral
        bool isAngleGreaterThanThreshold = indexAngle < angleThresholdIndex;

        // Si todos los dedos están extendidos, hay un movimiento hacia abajo y el ángulo es mayor que el umbral
        if (areThumbRingPinkyClosed && areIndexMiddleExtended && isAngleGreaterThanThreshold)
        {
            return true;
        }
    }

    return false;
}



    // Implementa esta función para obtener la posición de la mano en el fotograma anterior
    private Vector3 GetPreviousHandPosition()
    {
        // Implementa la lógica para obtener la posición de la mano en el fotograma anterior
        // Puedes almacenar la posición en una variable y actualizarla en cada fotograma
        // Asegúrate de inicializarla adecuadamente en el primer fotograma
        return Vector3.zero; // Cambia esto según tu implementación
    }



    Hand GetCurrentHand(Frame frame)
    {

        //Comprobamos que se detecte alguna mano
        if (frame.Hands.Count == 0)
        {
            handIdIsValid = false;
            return null;
        }
        else
        {

            //Si no teniamos alguna mano guardada de antemano, guardamos una
            if (!handIdIsValid || frame.Hand(handId) == null)
            {
                handId = frame.Hands[0].Id;
                handIdIsValid = true;
            }

            //Obtenemos la mano que queremos trackear
            return frame.Hand(handId);

        }

    }

    public void SetDesactivarTutorialConMovimiento(bool b)
    {
        desactivarTutorialConMovimiento = b;
    }
}
/*
bool AreAllFingersExtended(Hand hand)
{
    // Verifica que todos los dedos estén extendidos
    for (int i = 0; i < 5; i++)
    {
        if (!hand.Fingers[i].IsExtended)
        {
            return false;
        }
    }
    return true;
}
*/



