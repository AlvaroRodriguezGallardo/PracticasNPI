using System.Collections.Generic;
using UnityEngine;

public class DesplazarDropdowns : MonoBehaviour
{
    private List<DropdownGroup> dropdownGroups;

    void Start()
    {
        dropdownGroups = new List<DropdownGroup>(GetComponentsInChildren<DropdownGroup>());
    }

    void Update()
    {
        // Recorrer los DropdownGroup en orden inverso
        for (int i = dropdownGroups.Count - 1; i >= 0; i--)
        {
            var dropdownGroup = dropdownGroups[i];
            bool debajoDelDesplegado = i > 0 && dropdownGroups[i - 1].EstaDesplazado;

            // Desplazar solo si está debajo
            dropdownGroup.Desplazar(debajoDelDesplegado);
        }
    }
}
