using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ScriptScroll : MonoBehaviour
{
    private MainController mainController;
    void Start()
    {
        mainController = FindObjectOfType<MainController>();
    }
    // Update is called once per frame
    void Update()
    {

        if(mainController != null && mainController.DetectGestoScroll()){
            mainController.SimularClickSostenido(true);
        }
        else{
            mainController.SimularClickSostenido(false);
        }
    }
}
