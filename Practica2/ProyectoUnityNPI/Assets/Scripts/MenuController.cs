using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private Stack<GameObject> menuStack = new Stack<GameObject>();

    // Llamado cuando se avanza a un nuevo menú
    public void AvanzarAMenu(GameObject nuevoMenu)
    {
        if (nuevoMenu != null)
        {
            // Desactiva el menú actual si hay alguno
            if (menuStack.Count > 0)
            {
                GameObject menuActual = menuStack.Peek();
                menuActual.SetActive(false);
            }

            // Activa el nuevo menú y agrégalo a la pila
            nuevoMenu.SetActive(true);
            menuStack.Push(nuevoMenu);
        }
        else
        {
            Debug.LogWarning("Intento de avanzar a un menú nulo.");
        }
    }

    // Llamado cuando se desea retroceder al menú anterior
    public void RetrocederAMenuAnterior()
    {
        // Desactiva el menú actual
        if (menuStack.Count > 0)
        {
            GameObject menuActual = menuStack.Pop();
            menuActual.SetActive(false);
        }

        // Activa el menú anterior si hay alguno
        if (menuStack.Count > 0)
        {
            GameObject menuAnterior = menuStack.Peek();
            menuAnterior.SetActive(true);
        }
    }
}
