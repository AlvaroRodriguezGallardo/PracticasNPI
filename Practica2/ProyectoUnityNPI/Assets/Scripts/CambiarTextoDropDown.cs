using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CambiarTextoDropDown : MonoBehaviour
{
    public string textoDentro;
    public string TextoTitulo;
    public TMP_Dropdown dropdown;

    void Start()
    {
        // Configurar las opciones después de pulsar
        dropdown.ClearOptions();

        // Crear nuevas opciones personalizadas con TextMeshProUGUI
        List<TMP_Dropdown.OptionData> nuevasOpciones = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData(textoDentro),
            // Añade más opciones según sea necesario
        };

        // Añadir las nuevas opciones al Dropdown
        dropdown.AddOptions(nuevasOpciones);

        // Configurar el texto antes de pulsar (Label)

        // Cambiar el tamaño de la fuente de las opciones después de agregarlas
        
    }
    void Update(){
        dropdown.captionText.text = TextoTitulo;
    }

}
