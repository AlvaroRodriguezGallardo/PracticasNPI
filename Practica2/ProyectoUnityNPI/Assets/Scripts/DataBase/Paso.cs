using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StructPaso{

    public string nombre;
    public string descripcion;
    public int tramite;
    public Sprite foto;


}


public class BDPasos
{

    static Dictionary<int, StructPaso> pasos = new Dictionary<int, StructPaso>();
    
    public static void Insertar(int id, StructPaso datos){
        pasos.Add(id, datos);
    }

    public static void Eliminar(int id){
        pasos.Remove(id);
    }

    public static StructPaso Get(int id){
        return pasos[id];
    }

    public static int Count()
    {
        return pasos.Count;
    }
   
}