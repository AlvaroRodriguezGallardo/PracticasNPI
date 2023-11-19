// See https://aka.ms/new-console-template for more information
using System.Dynamic;
using System.Security.Claims;

class Tabla{
    public List<string> clavesPrimarias = new List<string>();
    public List<string> claves = new List<string>();
    public List<List<string>> tabla = new List<List<string>>();

    public void showClavesPrimarias(){
        string mensaje = "";
        foreach (string clave in clavesPrimarias){
            mensaje += clave + " ";
        }
        Console.WriteLine(mensaje);
    }

    public void showClaves(){
        string mensaje = "";
        foreach (string clave in claves){
            mensaje += clave + " ";
        }
        Console.WriteLine(mensaje);
    }

    public void insertar(List<string> tupla){
        if(clavesPrimarias.Count() == 0){
            Console.WriteLine("ERROR: La claves primarias no han sido declaradas");
        }
        else if(tupla.Count != clavesPrimarias.Count + claves.Count){
            Console.WriteLine("ERROR: Número de atributos incorrecto");
        }
        else{
            foreach(List<string> lista in tabla){
                if(String.Join(", ", lista.Take(clavesPrimarias.Count)) == String.Join(", ", tupla.Take(clavesPrimarias.Count))){
                    Console.WriteLine("ERROR: Ya existe una tupla con este conjunto de claves primarias");
                    Console.Write("Tupla en la posición ");
                    Console.Write(tabla.IndexOf(lista));
                    Console.Write(": [");
                    Console.Write(String.Join(", ", lista));
                    Console.Write("]\n");
                    return;
                }
            }
            tabla.Add(tupla);
            Console.Write("Tupla [");
            Console.Write(String.Join(", ", tupla));
            Console.Write("] insertada.\n");
        }
    }

    public void eliminar(int posicion){
        if(posicion < 0 || posicion > tabla.Count-1){
            Console.WriteLine("ERROR: No existe una tupla en esa posición");
        }
        else{
            Console.Write("Tupla [");
            Console.Write(String.Join(", ", tabla[posicion]));
            Console.Write("] se está eliminando.\n");
            tabla.RemoveAt(posicion);
        }
    }

    public void showTabla(){
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
    }

    public void declararClavesPrimarias(List<string> lista){
        if(tabla.Count == 0){
            clavesPrimarias.Clear();
            foreach(string clave in lista){
                clavesPrimarias.Add(clave);
            }
            Console.WriteLine("Claves primarias declaradas exitosamente");
        }
        else{
            Console.WriteLine("ERROR: Ya hay valores dentro de la tabla");
        }
    }

    public void declararClaves(List<string> lista){
        if(tabla.Count == 0){
            claves.Clear();
            foreach(string clave in lista){
                claves.Add(clave);
            }
            Console.WriteLine("Claves declaradas exitosamente");
        }
        else{
            Console.WriteLine("ERROR: Ya hay valores dentro de la tabla");
        }
    }
}

class Program{
    static void Main(string[] args){
        Tabla tabla = new Tabla();

        tabla.declararClaves(["Teléfono"]);
        tabla.insertar(["64736"]);
    }
}
