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

    static Dictionary<string, StructAlumnos> alumnos = new Dictionary<string, StructAlumnos>();

    public static void Insertar(string id, StructAlumnos datos){
        alumnos.Add(id, datos);
    }

    public static void Eliminar(string id){
        alumnos.Remove(id);
    }

    public static bool Exists(string id){
        return alumnos.ContainsKey(id);
    }
    public static StructAlumnos Get(string id){
            return alumnos[id];
    }
   
}
