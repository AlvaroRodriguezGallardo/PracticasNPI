using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StructAlumnos{

    public string nombre;
    public Horario horarios;

}


public class BDAlumnos
{

    static Dictionary<string, StructAlumnos> profesores = new Dictionary<string, StructAlumnos>();

    public static void Insertar(string id, StructAlumnos datos){
        profesores.Add(id, datos);
    }

    public static void Eliminar(string id){
        profesores.Remove(id);
    }

    public static StructAlumnos Get(string id){
        return profesores[id];
    }
   
}
