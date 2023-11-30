using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;

public class GeneradorBotones : MonoBehaviour
{
    [System.Serializable]   //Hacerlo serializable permite cambiar los valores de las instancias de esta clase desde el editor
    public struct ButtonStruct{
        public string nombreDia;
        public Sprite icon;
        public UnityEvent onClick; //Almacena las funciones que se ejecutan al pulsar un boton
    }
    public GameObject botonPrefab; // Prefab del botón
    public Transform contenedorBotones; // Contenedor para los botones

    // Lista de información de botones serializables
    private List<string> diasSemana = new List<string> { "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };
    private List<string> tiposMenu = new List<string> { "Normal", "Vegetariano", "Celíaco" };
    public List<ButtonStruct> listaDiasSemana;
    public List<ButtonStruct> listaTiposMenu;

    void Start()
    {
        // Generar los botones
        GenerarBotones();
    }

    void GenerarBotones()
    {
        // Espaciado entre botones
        float espacioEntreBotones = 250f;

        for(int i = 0; i < listaDiasSemana.Count; ++i){

            //Creamos el botón, basándonos en el prefab
            GameObject nuevoBoton = Instantiate(botonPrefab, Vector3.zero, Quaternion.identity, transform);

            //Calculamos su posición, y se la aplicamos
            Vector2 buttonPosition = new Vector2(espacioEntreBotones*i - 650f, -1000f);
            nuevoBoton.GetComponent<RectTransform>().anchoredPosition = buttonPosition;

            //Cambiamos el texto del botón
            nuevoBoton.GetComponentInChildren<TextMeshProUGUI>().text = diasSemana[i];

            int diaIndex = i; // Variable local para capturar el valor correcto en el cierre
            nuevoBoton.GetComponent<Button>().onClick.AddListener(() => BotonClicDiaSemana(diaIndex));

        }

        for(int i = 0; i < listaTiposMenu.Count; ++i){

            //Creamos el botón, basándonos en el prefab
            GameObject nuevoBoton = Instantiate(botonPrefab, Vector3.zero, Quaternion.identity, transform);

            //Calculamos su posición, y se la aplicamos
            Vector2 buttonPosition = new Vector2(espacioEntreBotones*i - 250f, 600f);
            nuevoBoton.GetComponent<RectTransform>().anchoredPosition = buttonPosition;

            //Cambiamos el texto del botón
            nuevoBoton.GetComponentInChildren<TextMeshProUGUI>().text = tiposMenu[i];

            int menuIndex = i; // Variable local para capturar el valor correcto en el cierre
            nuevoBoton.GetComponent<Button>().onClick.AddListener(() => BotonClicTipoMenu(menuIndex));

        }
    }

    // Función para manejar clics en los botones
    void BotonClicDiaSemana(int diaIndex)
    {
        Debug.Log("Se hizo clic en el botón del día: " + diasSemana[diaIndex]);
        // Aquí puedes agregar la lógica que deseas realizar cuando se hace clic en un botón.
    }
    void BotonClicTipoMenu(int menuIndex)
    {
        Debug.Log("Se hizo clic en el botón del tipo de menú: " + tiposMenu[menuIndex]);
        // Aquí puedes agregar la lógica que deseas realizar cuando se hace clic en un botón.
    }
}
