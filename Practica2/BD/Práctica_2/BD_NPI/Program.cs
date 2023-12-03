// See https://aka.ms/new-console-template for more information
using System.Dynamic;
using System.Security.Claims;

class Tabla{
    public List<string> ClavesPrimarias {get; set;} = new List<string>();
    public List<string> Claves {get; set;} = new List<string>();
    public List<List<string>> Table {get; set;} = new List<List<string>>();

    public void ShowClavesPrimarias(){
        string mensaje = "";
        foreach (string clave in ClavesPrimarias){
            mensaje += clave + " ";
        }
        Console.WriteLine(mensaje);
    }

    public void ShowClaves(){
        string mensaje = "";
        foreach (string clave in Claves){
            mensaje += clave + " ";
        }
        Console.WriteLine(mensaje);
    }

    public void Insertar(List<string> tupla){
        if(ClavesPrimarias.Count() == 0){
            Console.WriteLine("ERROR: La Claves primarias no han sido declaradas");
        }
        else if(tupla.Count != ClavesPrimarias.Count + Claves.Count){
            Console.WriteLine("ERROR: Número de atributos incorrecto");
        }
        else{
            foreach(List<string> lista in Table){
                if(String.Join(", ", lista.Take(ClavesPrimarias.Count)) == String.Join(", ", tupla.Take(ClavesPrimarias.Count))){
                    Console.WriteLine("ERROR: Ya existe una tupla con este conjunto de Claves primarias");
                    Console.Write("Tupla en la posición ");
                    Console.Write(Table.IndexOf(lista));
                    Console.Write(": [");
                    Console.Write(String.Join(", ", lista));
                    Console.Write("]\n");
                    return;
                }
            }
            Table.Add(tupla);
            Console.Write("Tupla [");
            Console.Write(String.Join(", ", tupla));
            Console.Write("] insertada.\n");
        }
    }

    public void Eliminar(int posicion){
        if(posicion < 0 || posicion > Table.Count-1){
            Console.WriteLine("ERROR: No existe una tupla en esa posición");
        }
        else{
            Console.Write("Tupla [");
            Console.Write(String.Join(", ", Table[posicion]));
            Console.Write("] se está eliminando.\n");
            Table.RemoveAt(posicion);
        }
    }

    public void ShowTabla(){
        Console.Write("[");
        Console.Write(String.Join(", ", ClavesPrimarias));
        Console.Write(", ");
        Console.Write(String.Join(", ", Claves));
        Console.Write("]\n");
        foreach(List<string> linea in Table){
            Console.Write("[");
            Console.Write(String.Join(", ", linea));
            Console.Write("]\n");
        }
    }

    public void DeclararClavesPrimarias(List<string> lista){
        if(Table.Count == 0){
            ClavesPrimarias.Clear();
            foreach(string clave in lista){
                ClavesPrimarias.Add(clave);
            }
            Console.WriteLine("Claves primarias declaradas exitosamente");
        }
        else{
            Console.WriteLine("ERROR: Ya hay valores dentro de la tabla");
        }
    }

    public void DeclararClaves(List<string> lista){
        if(Table.Count == 0){
            Claves.Clear();
            foreach(string clave in lista){
                Claves.Add(clave);
            }
            Console.WriteLine("Claves declaradas exitosamente");
        }
        else{
            Console.WriteLine("ERROR: Ya hay valores dentro de la Tabla");
        }
    }

    public List<string> Buscar(List<string> tupla){
        if(ClavesPrimarias.Count() == 0){
            Console.WriteLine("ERROR: La Claves primarias no han sido declaradas");
        }
        else if(tupla.Count != ClavesPrimarias.Count + Claves.Count){
            Console.WriteLine("ERROR: Número de atributos incorrecto");
        }
        else{
            foreach(List<string> lista in Table){
                if(String.Join(", ", lista.Take(ClavesPrimarias.Count)) == String.Join(", ", tupla.Take(ClavesPrimarias.Count))){
                    return lista;
                }
            }
            Console.Write("No existe la tupla con Claves primarias [");
            Console.Write(String.Join(", ", tupla));
            Console.Write("] insertada.\n");
        }
        return [];
    }
}

class Program{
    static void Main(string[] args){
        Tabla Tabla = new Tabla();

        Tabla.DeclararClavesPrimarias(["Teléfono"]);
        Tabla.Insertar(["64736"]);
        List<string> elemento = Tabla.Buscar(["1"]);
        Console.WriteLine(elemento);
        elemento = Tabla.Buscar(["64736"]);
        Console.WriteLine(elemento);
    }
}
