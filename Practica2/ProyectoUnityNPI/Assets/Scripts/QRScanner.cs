using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using TMPro;

public class QRScanner : MonoBehaviour
{


    public RawImage rawImageBackground;
    public AspectRatioFitter aspectRatioFitter;

    bool isCamAvaiable;
    WebCamTexture cameraTexture;


    // Start is called before the first frame update
    void Start()
    {

        SetUpCamera();
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
    }

    void SetUpCamera(){

        WebCamDevice [] devices = WebCamTexture.devices;

        if(devices.Length == 0){
            isCamAvaiable = false;
            return;
        }

        for(int i = 0; i < devices.Length; ++i){

            //if(devices[i].isFrontFacing == false){
                cameraTexture = new WebCamTexture(devices[i].name);
                break;
            //}


        }

        cameraTexture.Play();
        rawImageBackground.texture = cameraTexture;
        isCamAvaiable = true;

    }

    void UpdateCameraRender(){
        if(isCamAvaiable == false) return;

        float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
        aspectRatioFitter.aspectRatio = ratio;

        int orientation = -cameraTexture.videoRotationAngle;
        rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    public void OnClickScan(){
        Scan();
    }
    public string Scan(){

        try{

            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);

            if(result != null){
                return result.Text;
                //Debug.Log(result.Text);
            }
            else{
                return null;
                //textOut.text = "Failed to read QR code";
            }

        }
        catch{

            Debug.LogError("ERROR EN EL SCAN DE QR");
            return null;

        }


    }
}
