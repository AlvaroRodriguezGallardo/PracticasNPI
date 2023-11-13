using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlCambioMenus : MonoBehaviour
{
    // Start is called before the first frame update
    public MenuController menuController;
    [SerializeField]
    private GameObject TuProximoMenuGameObject;

    private static ControlCambioMenus _instance;

    public static ControlCambioMenus Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ControlCambioMenus>();
            }
            return _instance;
        }
    }

    private void Start()
    {
        // Asegúrate de asignar tu controlador de menú en el Inspector
        if (menuController == null)
        {
            Debug.LogError("Asigna el controlador de menú en el Inspector.");
        }
    }

    // Esta función se asigna al evento de clic del botón en el Inspector
    public void OnBotonClic()
    {
        // Llama a AvanzarAMenu cuando el botón se hace clic
        if (menuController != null)
        {
            menuController.AvanzarAMenu(TuProximoMenuGameObject);
        }
    }
}
