using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicializarBD : MonoBehaviour
{

    [SerializeField]
    public List<StructProfesor> profesores;
    public List<StructAlumnos> alumnos;
    public List<StructPaso> pasos;

    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < profesores.Count; ++i){

            BDProfesores.Insertar(i.ToString(), profesores[i]);

        } 

        for(int i = 0; i < alumnos.Count; ++i){

            BDAlumnos.Insertar(i.ToString(), alumnos[i]);

        }

        for (int i = 0; i < pasos.Count; ++i)
        {

            BDPasos.Insertar(i, pasos[i]);

        }


    }

    
}
