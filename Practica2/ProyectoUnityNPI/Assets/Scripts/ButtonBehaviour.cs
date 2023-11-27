using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonController : MonoBehaviour
{
    private void Start()
    {
        // Intenta obtener la referencia al componente TextMeshProUGUI del botón
        TextMeshProUGUI buttonText = GetComponentInChildren<TextMeshProUGUI>();

        // Verifica si se encontró el componente TextMeshProUGUI
        if (buttonText != null)
        {
            // Ajusta la alineación del texto al centro o abajo, según tus preferencias
            buttonText.alignment = TextAlignmentOptions.Center; // o TextAlignmentOptions.Bottom

            // Ajusta el anclaje del texto
            RectTransform textTransform = buttonText.GetComponent<RectTransform>();
            textTransform.anchorMin = new Vector2(0.5f, 0f); // Establece el anclaje mínimo (izquierda, abajo)
            textTransform.anchorMax = new Vector2(0.5f, 0f); // Establece el anclaje máximo (derecha, abajo)
            textTransform.anchoredPosition = new Vector2(0f, -50f); // Ajusta la posición vertical según tus necesidades

            // Configura el pivot para centrar el texto horizontalmente
            textTransform.pivot = new Vector2(0.5f, 0.5f);

            // Ajusta el tamaño del texto
            buttonText.fontSize = 150; // Ajusta el tamaño del texto según tus necesidades
            Debug.Log("Tamaño del Texto: " + buttonText.fontSize);
        }
        else
        {
            // Si no se encuentra el componente, puedes imprimir un mensaje de advertencia
            Debug.LogWarning("El botón no tiene un componente TextMeshProUGUI asociado.");
        }
    }
}
