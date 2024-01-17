using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuDesplegable : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject panelDesplegable;
    private bool desplegado = false;

    public void ToggleDesplegable()
    {
        desplegado = !desplegado;
        panelDesplegable.SetActive(desplegado);
    }
}
