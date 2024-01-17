using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;
using System.IO;

public class GeneradorBotones : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonStruct
    {
        public string nombreDia;
        public string nombreTipoMenu;
        public UnityEvent onClick;
    }

    public GameObject botonPrefab;
    public Transform contenedorBotones;

    private List<string> diasSemana = new List<string> { "Lunes", "Martes", "Miercoles", "Jueves", "Viernes", "Sabado" };
    private List<string> tiposMenu = new List<string> { "Normal", "Vegetariano", "Celiaco" };
    private int tipoMenuSeleccionado = 0;
    private int diaSemanaSeleccionado = 0;
    public GameObject menu;

    // Lista de objetos asociados a cada combinación de día y menú
    public List<GameObject> objetosAsociados;

    void Start()
    {
        Generarmenus();
        GenerarBotones();
    }

    void GenerarBotones()
    {
        float espacioEntreBotones = 250f;

        for (int i = 0; i < diasSemana.Count; ++i)
        {
            GameObject nuevoBoton = Instantiate(botonPrefab, Vector3.zero, Quaternion.identity, transform);

            Vector2 buttonPosition = new Vector2(espacioEntreBotones * i * 1.20f - 750f, -1200f);
            nuevoBoton.GetComponent<RectTransform>().anchoredPosition = buttonPosition;

            nuevoBoton.GetComponentInChildren<TextMeshProUGUI>().text = diasSemana[i];

            int diaIndex = i;
            nuevoBoton.GetComponent<Button>().onClick.AddListener(() => BotonClicDiaSemana(diaIndex));
        }

        for (int i = 0; i < tiposMenu.Count; ++i)
        {
            GameObject nuevoBoton = Instantiate(botonPrefab, Vector3.zero, Quaternion.identity, transform);

            Vector2 buttonPosition = new Vector2(espacioEntreBotones * i * 1.20f - 350f, 400f);
            nuevoBoton.GetComponent<RectTransform>().anchoredPosition = buttonPosition;

            nuevoBoton.GetComponentInChildren<TextMeshProUGUI>().text = tiposMenu[i];

            int menuIndex = i;
            nuevoBoton.GetComponent<Button>().onClick.AddListener(() => BotonClicTipoMenu(menuIndex));
        }
    }

    void BotonClicDiaSemana(int diaIndex)
    {
        Debug.Log("Se hizo clic en el botón del día: " + diasSemana[diaIndex]);

        diaSemanaSeleccionado = diaIndex;

        ActivarObjetoCorrespondiente();
    }

    void BotonClicTipoMenu(int menuIndex)
    {
        Debug.Log("Se hizo clic en el botón del tipo de menú: " + tiposMenu[menuIndex]);

        tipoMenuSeleccionado = menuIndex;

        ActivarObjetoCorrespondiente();
    }

    void ActivarObjetoCorrespondiente()
    {
        // Buscar el objeto asociado a la combinación de día y menú
        string nombreObjeto = $"{diasSemana[diaSemanaSeleccionado]}_{tiposMenu[tipoMenuSeleccionado]}";
        GameObject objetoAsociado = objetosAsociados.Find(obj => obj.name == nombreObjeto);

        // Activar el objeto encontrado y desactivar los demás
        foreach (var obj in objetosAsociados)
        {
            obj.SetActive(obj == objetoAsociado);
        }
    }

    void Generarmenus()
    {
        // Asignar referencias a objetos asociados
        objetosAsociados = new List<GameObject>();
        for (int i = 0; i < diasSemana.Count; ++i)
        {
            for (int j = 0; j < tiposMenu.Count; ++j)
            {
                string nombreObjeto = $"{diasSemana[i]}_{tiposMenu[j]}";
                GameObject objeto = GameObject.Find(nombreObjeto);

                if (objeto != null)
                {
                    objetosAsociados.Add(objeto);
                    objeto.SetActive(false);
                }
                else
                {
                    Debug.LogError($"Objeto {nombreObjeto} no encontrado.");
                }
            }
        }
        objetosAsociados[0].SetActive(true);
    }
}
