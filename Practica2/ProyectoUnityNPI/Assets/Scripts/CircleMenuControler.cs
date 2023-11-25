using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

using TMPro;

public class CircleMenuControler : MonoBehaviour
{

    [System.Serializable]   //Hacerlo serializable permite cambiar los valores de las instancias de esta clase desde el editor
    public struct ButtonStruct{

        public string text;
        public Sprite icon;
        public UnityEvent onClick; //Almacena las funciones que se ejecutan al pulsar un boton


    }

    public float radious = 1000; //Radio del circulo que forman los botones, en pixeles.
    public float YMultiplier = 1.5f;
    public GameObject buttonPrefab; //
    public List<ButtonStruct> buttonsProperties; //Las propiedades que tendrá cada botón
    public UnityEvent accionesGestoVolver;
    private MainController mainController;
    float lastGestureDetectionTime = 0f;
    float gestureCooldown = 1.5f;  // Ajusta según sea necesario
    

    // Start is called before the first frame update
    void Start()
    {
        mainController = FindObjectOfType<MainController>();
        //Recorremos la lista con las propiedades de cada botón
        for(int i = 0; i < buttonsProperties.Count; ++i){

            //Creamos el botón, basándonos en el prefab
            GameObject nuevoObjeto = Instantiate(buttonPrefab, Vector3.zero, Quaternion.identity, transform);

            //Calculamos su posición, y se la aplicamos
            Vector2 buttonPosition = new Vector2(radious * Mathf.Sin(-2f * Mathf.PI * i / buttonsProperties.Count + 2f * Mathf.PI * 0.5f / buttonsProperties.Count), 
                                    YMultiplier * radious * Mathf.Cos(-2f * Mathf.PI * i / buttonsProperties.Count + 2f * Mathf.PI * 0.5f / buttonsProperties.Count));
            nuevoObjeto.GetComponent<RectTransform>().anchoredPosition = buttonPosition;

            //Cambiamos el texto del botón
            nuevoObjeto.GetComponentInChildren<TextMeshProUGUI>().text = buttonsProperties[i].text;

            //Cambiamos su icono
            nuevoObjeto.transform.Find("Image").GetComponent<Image>().sprite = buttonsProperties[i].icon;

            //Añadimos las funciones que se van a ejecutar al clickar el botón
            nuevoObjeto.GetComponent<Button>().onClick.AddListener(buttonsProperties[i].onClick.Invoke);

        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mainController.DetectGestoPersonalizado()){
            if (Time.time - lastGestureDetectionTime > gestureCooldown)
            {
                Debug.Log("Deberia volver hacia atras");
                // Realizar la acción de volver al menú anterior
                accionesGestoVolver.Invoke();
                // Actualizar el estado de la detección y el tiempo
                lastGestureDetectionTime = Time.time;
            }
        }
    }
}
