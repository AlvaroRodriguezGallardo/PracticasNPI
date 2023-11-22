using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicializarBD : MonoBehaviour
{

    [SerializeField]
    public List<StructProfesor> profesores;

    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < profesores.Count; ++i){

            BDProfesores.Insertar(i.ToString(), profesores[i]);

        } 


    }

    
}
