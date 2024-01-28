using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using UnityEngine.Events;
using TMPro;
using System.IO;

public class GeneradorBotonesPlan : MonoBehaviour
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

    private List<string> Menciones = new List<string> { "CSI", "IC", "IS", "SI", "TI"};
    private List<string> Cursos = new List<string> { "Primero", "Segundo", "Tercero", "Cuarto" };
    private int mencionSeleccionada = 0;
    private int cursoSeleccionado = 0;
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

        for (int i = 0; i < Menciones.Count; ++i)
        {
            GameObject nuevoBoton = Instantiate(botonPrefab, Vector3.zero, Quaternion.identity, transform);

            Vector2 buttonScale = new Vector3(1.5f, 2, 1);

            nuevoBoton.GetComponent<RectTransform>().localScale = buttonScale;

            Vector2 buttonPosition = new Vector2(espacioEntreBotones * i * 1.50f - 750f, -1000f);
            nuevoBoton.GetComponent<RectTransform>().anchoredPosition = buttonPosition;

            // Configurar el texto del botón
            TextMeshProUGUI buttonText = nuevoBoton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = Menciones[i];
    
            // Aumentar el tamaño de fuente aquí
            buttonText.fontSize = 36; 

            int mencionIndex = i;
            nuevoBoton.GetComponent<Button>().onClick.AddListener(() => BotonClicMencion(mencionIndex));
        }

        for (int i = 0; i < Cursos.Count; ++i)
        {
            GameObject nuevoBoton = Instantiate(botonPrefab, Vector3.zero, Quaternion.identity, transform);

            Vector2 buttonScale = new Vector3(1.5f, 2, 1);

            nuevoBoton.GetComponent<RectTransform>().localScale = buttonScale;

            Vector2 buttonPosition = new Vector2(espacioEntreBotones * i * 1.50f - 550f, 700f);
            nuevoBoton.GetComponent<RectTransform>().anchoredPosition = buttonPosition;

            // Configurar el texto del botón
            TextMeshProUGUI buttonText = nuevoBoton.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = Cursos[i];
    
            // Aumentar el tamaño de fuente aquí
            buttonText.fontSize = 36; // Cambia 30 al tamaño de fuente que desees

            int cursoIndex = i;
            nuevoBoton.GetComponent<Button>().onClick.AddListener(() => BotonClicCurso(cursoIndex));
        }
    }

    void BotonClicMencion(int mencionIndex)
    {
        Debug.Log("Se hizo clic en el botón del día: " + Menciones[mencionIndex]);

        mencionSeleccionada = mencionIndex;

        ActivarObjetoCorrespondiente();
    }

    void BotonClicCurso(int cursoIndex)
    {
        Debug.Log("Se hizo clic en el botón del tipo de menú: " + Cursos[cursoIndex]);

        cursoSeleccionado = cursoIndex;

        ActivarObjetoCorrespondiente();
    }

    void ActivarObjetoCorrespondiente()
    {
        // Buscar el objeto asociado a la combinación de día y menú
        string nombreObjeto = $"{Menciones[mencionSeleccionada]}_{Cursos[cursoSeleccionado]}";
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
        for (int i = 0; i < Menciones.Count; ++i)
        {
            for (int j = 0; j < Cursos.Count; ++j)
            {
                string nombreObjeto = $"{Menciones[i]}_{Cursos[j]}";
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

    public void AccionGestoSiguiente(){
        BotonClicMencion((mencionSeleccionada + 1)%5);
    }

    public void AccionGestoAnterior(){
        BotonClicMencion((mencionSeleccionada + 4)%5);
    }
}