using System.Collections;
using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoProfesoresController : MonoBehaviour
{

    public Sprite iconoPredeterminado;
    public Sprite despachoPredeterminado;

    public void MostrarInfoProfesor(string id){

        StructProfesor datos = BDProfesores.Get(id);

        transform.Find("Nombre").GetComponent<TextMeshProUGUI>().text = datos.nombre;
        transform.Find("Correo").GetComponent<TextMeshProUGUI>().text = "Correo: "+ datos.email;
        transform.Find("Telefono").GetComponent<TextMeshProUGUI>().text = "Teléfono: "+ datos.telefono;

        if(datos.foto != null)
            transform.Find("Foto").GetComponent<Image>().sprite = datos.foto;
        else    
            transform.Find("Foto").GetComponent<Image>().sprite = iconoPredeterminado;

        transform.Find("QR_info").GetComponent<Image>().sprite = datos.qr_info;
        if(datos.qr_despacho != null)
            transform.Find("QR_despacho").GetComponent<Image>().sprite = datos.qr_despacho;
        else
            transform.Find("QR_despacho").GetComponent<Image>().sprite = despachoPredeterminado;



    }

}
