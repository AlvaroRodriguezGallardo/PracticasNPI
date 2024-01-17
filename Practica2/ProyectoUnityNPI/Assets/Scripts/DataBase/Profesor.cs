using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StructProfesor{

    public string nombre;
    public string email;
    public string telefono;
    public Sprite foto;
    public Sprite qr_info;
    
    [SerializeField]
    public Horario horarioClases;

}


public class BDProfesores
{

    static Dictionary<string, StructProfesor> profesores = new Dictionary<string, StructProfesor>();

    public static void Insertar(string id, StructProfesor datos){
        profesores.Add(id, datos);
    }

    public static void Eliminar(string id){
        profesores.Remove(id);
    }

    public static bool Exists(string id){
        return profesores.ContainsKey(id);
    }

    public static StructProfesor Get(string id){
        return profesores[id];
    }
   
}
