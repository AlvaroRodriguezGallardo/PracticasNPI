// See https://aka.ms/new-console-template for more information
using System.Dynamic;
using System.Security.Claims;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

class Tabla{
    public List<string> clavesPrimarias = new List<string>();
    public List<string> claves = new List<string>();
    public List<List<string>> tabla = new List<List<string>>();

    public void showClavesPrimarias(){
        string mensaje = "";
        foreach (string clave in clavesPrimarias){
            mensaje += clave + " ";
        }
        Debug.Log(mensaje);
    }

    public void showClaves(){
        string mensaje = "";
        foreach (string clave in claves){
            mensaje += clave + " ";
        }
        Debug.Log(mensaje);
    }

    /*public void insertar(List<string> tupla){
        if(clavesPrimarias.Count == 0){
            Debug.LogError("ERROR: La claves primarias no han sido declaradas");
        }
        else if(tupla.Count != clavesPrimarias.Count + claves.Count){
            Debug.LogError("ERROR: Número de atributos incorrecto");
        }
        else{
            foreach(List<string> lista in tabla){
                if(string.Join(", ", lista.Take(clavesPrimarias.Count)) == string.Join(", ", tupla.Take(clavesPrimarias.Count))){
                    Debug.LogError("ERROR: Ya existe una tupla con este conjunto de claves primarias");
                    Debug.LogError(string.Join("Tupla en la posición ", tabla.IndexOf(lista), ": [, ", lista, "]\n"));

                    return;
                }
            }
            tabla.Add(tupla);
            Debug.Log(string.Join("Tupla [, ", tupla, "] insertada.\n"));

        }
    }*/

    public void eliminar(int posicion){
        if(posicion < 0 || posicion > tabla.Count-1){
            Debug.LogError("ERROR: No existe una tupla en esa posición");
        }
        else{
            Debug.Log(string.Join("Tupla [", tabla[posicion], "] se está eliminando.\n"));
            tabla.RemoveAt(posicion);
        }
    }

    /*public void showTabla(){
        Console.Write("[");
        Console.Write(String.Join(", ", clavesPrimarias));
        Console.Write(", ");
        Console.Write(String.Join(", ", claves));
        Console.Write("]\n");
        foreach(List<string> linea in tabla){
            Console.Write("[");
            Console.Write(String.Join(", ", linea));
            Console.Write("]\n");
        }
    }*/

    public void declararClavesPrimarias(List<string> lista){
        if(tabla.Count == 0){
            clavesPrimarias.Clear();
            foreach(string clave in lista){
                clavesPrimarias.Add(clave);
            }
            Debug.Log("Claves primarias declaradas exitosamente");
        }
        else{
            Debug.LogError("ERROR: Ya hay valores dentro de la tabla");
        }
    }

    public void declararClaves(List<string> lista){
        if(tabla.Count == 0){
            claves.Clear();
            foreach(string clave in lista){
                claves.Add(clave);
            }
            Debug.Log("Claves declaradas exitosamente");
        }
        else{
            Debug.LogError("ERROR: Ya hay valores dentro de la tabla");
        }
    }
}

