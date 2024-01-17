using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuHorariosController : MonoBehaviour
{


    public GameObject menuQRObject;
    public GameObject scheduleObject;


    public TextMeshProUGUI obLunes, obMartes, obMiercoles, obJueves, obViernes;

    bool lookingforQR;
    QRScanner qrScanner;

    string scannedText;

    // Start is called before the first frame update

    void Start(){
        qrScanner = menuQRObject.GetComponentInChildren<QRScanner>();



        obLunes = scheduleObject.transform.Find("Lunes").Find("MainText").GetComponent<TextMeshProUGUI>();
        obMartes = scheduleObject.transform.Find("Martes").Find("MainText").GetComponent<TextMeshProUGUI>();
        obMiercoles = scheduleObject.transform.Find("Miercoles").Find("MainText").GetComponent<TextMeshProUGUI>();
        obJueves = scheduleObject.transform.Find("Jueves").Find("MainText").GetComponent<TextMeshProUGUI>();
        obViernes = scheduleObject.transform.Find("Viernes").Find("MainText").GetComponent<TextMeshProUGUI>();


    }
    void OnEnable()
    {

        lookingforQR = true;
        menuQRObject.SetActive(true);
        scheduleObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (lookingforQR)
        {

            scannedText = qrScanner.Scan();

            if (scannedText != null)
            {

                if (scannedText != null && BDAlumnos.Exists(scannedText))
                {


                    lookingforQR = false;
                    ShowSchedule(scannedText);

                }

            }

        }
    }

    void ShowSchedule(string id){


        menuQRObject.SetActive(false);
        scheduleObject.SetActive(true);

        //Temporal para testear
        scheduleObject.GetComponentInChildren<TextMeshProUGUI>().text = "Horario de " + id;

        Debug.Log("Mostrando horario " + id.ToString());

        StructAlumnos infoAlumno = BDAlumnos.Get(id);
        
        menuQRObject.SetActive(false);
        scheduleObject.SetActive(true);

        scheduleObject.GetComponentInChildren<TextMeshProUGUI>().text = "Horario de " + infoAlumno.nombre;

        string aux = "";

        foreach(Horario.SlotHorario slot in infoAlumno.horarios.GetDia(Horario.DiaSemana.Lunes)){

            aux += slot.inicio.toString() + "-" + slot.fin.toString() + "\n" + slot.nombreClase + "\n\n";

        }
        obLunes.text = aux;

        aux = "";
        foreach(Horario.SlotHorario slot in infoAlumno.horarios.GetDia(Horario.DiaSemana.Martes)){

            aux += slot.inicio.toString() + "-" + slot.fin.toString() + "\n" + slot.nombreClase + "\n\n";

        }
        obMartes.text = aux;

        aux = "";
        foreach(Horario.SlotHorario slot in infoAlumno.horarios.GetDia(Horario.DiaSemana.Miercoles)){

            aux += slot.inicio.toString() + "-" + slot.fin.toString() + "\n" + slot.nombreClase + "\n\n";

        }
        obMiercoles.text = aux;

        aux = "";
        foreach(Horario.SlotHorario slot in infoAlumno.horarios.GetDia(Horario.DiaSemana.Jueves)){

            aux += slot.inicio.toString() + "-" + slot.fin.toString() + "\n" + slot.nombreClase + "\n\n";

        }
        obJueves.text = aux;

        aux = "";
        foreach(Horario.SlotHorario slot in infoAlumno.horarios.GetDia(Horario.DiaSemana.Viernes)){

            aux += slot.inicio.toString() + "-" + slot.fin.toString() + "\n" + slot.nombreClase + "\n\n";

        }
        obViernes.text = aux;


    }
}
