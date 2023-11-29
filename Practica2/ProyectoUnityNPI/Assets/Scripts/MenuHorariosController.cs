using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuHorariosController : MonoBehaviour
{


    public GameObject menuQRObject;
    public GameObject scheduleObject;

    bool lookingforQR;
    QRScanner qrScanner;

    string scannedText;

    // Start is called before the first frame update

    void Start(){
        qrScanner = menuQRObject.GetComponentInChildren<QRScanner>();
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

        if(lookingforQR){

            scannedText = qrScanner.Scan();
            if(scannedText != null){

                lookingforQR = false;
                ShowSchedule(scannedText);

            }

        }
        
    }

    void ShowSchedule(string id){

        menuQRObject.SetActive(false);
        scheduleObject.SetActive(true);

        //Temporal para testear
        scheduleObject.GetComponentInChildren<TextMeshProUGUI>().text = "Horario de " + id;

        Debug.Log("Mostrando horario " + id.ToString());

    }
}
