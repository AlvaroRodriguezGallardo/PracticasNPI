using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoPasosController : MonoBehaviour
{

    public Sprite iconoPredeterminado;
    public int numeroInicio = -1;
    public int tramite = -1;
    
    public void MostrarSiguientePaso(){

        if (numeroInicio < BDPasos.Count()-1 && tramite == BDPasos.Get(numeroInicio+1).tramite)
        {
            numeroInicio = numeroInicio + 1;
            StructPaso datos = BDPasos.Get(numeroInicio);

            transform.Find("Nombre").GetComponent<TextMeshProUGUI>().text = datos.nombre;
            transform.Find("Descripcion").GetComponent<TextMeshProUGUI>().text = datos.descripcion;

            if (datos.foto != null)
            {
                transform.Find("Foto").GetComponent<Image>().sprite = datos.foto;
            }
            else
            {
                transform.Find("Foto").GetComponent<Image>().sprite = iconoPredeterminado;
            }
        }

    }

    public void MostrarAnteriorPaso()
    {

        if (numeroInicio > 0 && tramite == BDPasos.Get(numeroInicio-1).tramite)
        {
            numeroInicio = numeroInicio - 1;
            StructPaso datos = BDPasos.Get(numeroInicio);

            transform.Find("Nombre").GetComponent<TextMeshProUGUI>().text = datos.nombre;
            transform.Find("Descripcion").GetComponent<TextMeshProUGUI>().text = datos.descripcion;

            if (datos.foto != null)
            {
                transform.Find("Foto").GetComponent<Image>().sprite = datos.foto;
            }
            else
            {
                transform.Find("Foto").GetComponent<Image>().sprite = iconoPredeterminado;
            }
        }

    }

    public void reiniciar()
    {
        tramite = -1;
        numeroInicio = -1;
    }

    public void setNumeroInicio(int i)
    {
        numeroInicio = i;
    }

    public void setTramite(int i)
    {
        tramite = i;
        do
        {
            numeroInicio = numeroInicio + 1;
        } while (tramite != BDPasos.Get(numeroInicio).tramite);
        numeroInicio = numeroInicio - 1;
    }


}
