using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CambiarTamanoTextoDropdown : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public int tamanoFuenteOpciones = 14;  // Ajusta el tamaño de la fuente según sea necesario

    void Start()
    {
        // Verifica si el componente TMP_Dropdown está asignado
        if (dropdown != null)
        {
            // Accede al componente TextMeshProUGUI dentro del Dropdown
            TMP_Text textoDropdown = dropdown.GetComponentInChildren<TMP_Text>();

            // Cambia el tamaño de la fuente de las opciones del Dropdown
            if (textoDropdown != null)
            {
                textoDropdown.fontSize = tamanoFuenteOpciones;
            }
            else
            {
                Debug.LogError("No se encontró el componente TextMeshProUGUI dentro del Dropdown.");
            }
        }
        else
        {
            Debug.LogError("El campo 'dropdown' no está asignado en el script CambiarTamañoTextoDropdown.");
        }
    }
}
