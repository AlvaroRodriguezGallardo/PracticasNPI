using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X_Gesture : MonoBehaviour
{
    public Transform leftHand;
    public Transform rightHand;
    public float intersectionThreshold = 0.1f;

    private void Update()
    {
        if (DetectXGesture())
        {
            Vector3 intersectionPoint = CalculateIntersectionPoint(leftHand.position, rightHand.position);
            if (IsNearWristIntersection(leftHand.position, rightHand.position, intersectionPoint))
            {
                // Ejecuta la acción de "Volver a la pestaña anterior"
                Debug.Log("Volver a la pestaña anterior");
            }
        }
    }

    bool DetectXGesture()
    {
        // Lógica para detectar una 'X' formada por las palmas de las manos
        // Puedes utilizar la posición y orientación de las manos para esto
        // Retorna true si se detecta la 'X', de lo contrario, false
        return false;
    }

    Vector3 CalculateIntersectionPoint(Vector3 pointA, Vector3 pointB)
    {
        // Calcula el punto de intersección entre dos puntos
        return (pointA + pointB) / 2f;
    }

    bool IsNearWristIntersection(Vector3 wristLeft, Vector3 wristRight, Vector3 intersectionPoint)
    {
        // Determina si el punto de intersección está cerca de las muñecas
        float distanceLeft = Vector3.Distance(wristLeft, intersectionPoint);
        float distanceRight = Vector3.Distance(wristRight, intersectionPoint);

        return distanceLeft < intersectionThreshold && distanceRight < intersectionThreshold;
    }
}
