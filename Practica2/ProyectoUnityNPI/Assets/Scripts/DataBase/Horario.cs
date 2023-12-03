using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Horario
{
    [System.Serializable]    
    public struct SlotHorario{

        [System.Serializable]
        public class Hora{

            [SerializeField]
            int hora;
            [SerializeField]
            int minuto;

            public Hora(int hora, int minuto){
                Set(hora, minuto);
            }
            void Validar(){
                hora += minuto / 60;
                minuto %= 60;

                hora %= 60;
            } 

            public void Set(int hora, int minuto){
                this.hora = hora;
                this.minuto = minuto;

                Validar();
            }
            public int GetHora(){ return hora;}
            public int GetMinuto(){ return minuto;}
            public string toString(){return hora+":"+minuto;}

            public static bool operator <(Hora left, Hora right){
                if(left.hora == right.hora)
                    return left.minuto < right.minuto;
                
                return left.hora < right.hora;
            }
            public static bool operator >(Hora left, Hora right){
                if(left.hora == right.hora)
                    return left.minuto > right.minuto;
                
                return left.hora > right.hora;
            }

        }

        public string nombreClase;
        public Hora inicio;
        public Hora fin;

        public static bool operator <(SlotHorario left, SlotHorario right){ return left.inicio < right.inicio;}
        public static bool operator >(SlotHorario left, SlotHorario right){ return left.inicio > right.inicio;}
        

    }

    [System.Serializable]
    public enum DiaSemana{

        Lunes = 0,
        Martes = 1,
        Miercoles = 2,
        Jueves = 3,
        Viernes = 4,
        Sabado = 5,
        Domingo = 6

    }
    
    //Hago esto en vez de una lista de listas para poder mostrar los campos en el editor de Unity
    //No es lo mejor, pero nos hará la vida más fácil
    public List<SlotHorario> lunes;
    public List<SlotHorario> martes;
    public List<SlotHorario> miercoles;
    public List<SlotHorario> jueves;
    public List<SlotHorario> viernes;

    public void Add(DiaSemana dia, SlotHorario infoClase){

        switch(dia){
            case DiaSemana.Lunes: lunes.Add(infoClase); break;  
            case DiaSemana.Martes: martes.Add(infoClase); break;
            case DiaSemana.Miercoles: miercoles.Add(infoClase); break;
            case DiaSemana.Jueves: jueves.Add(infoClase); break;
            case DiaSemana.Viernes: viernes.Add(infoClase); break;
        }


    }

    public List<SlotHorario> GetDia(DiaSemana dia){
        
        switch(dia){
            case DiaSemana.Lunes: return lunes;   
            case DiaSemana.Martes: return martes; 
            case DiaSemana.Miercoles: return miercoles; 
            case DiaSemana.Jueves: return jueves;
            case DiaSemana.Viernes: return viernes;
        }

        return null;

    }

    public void Remove(DiaSemana dia, SlotHorario infoClase){

        switch(dia){
            case DiaSemana.Lunes: lunes.Remove(infoClase); break;  
            case DiaSemana.Martes: martes.Remove(infoClase); break;
            case DiaSemana.Miercoles: miercoles.Remove(infoClase); break;
            case DiaSemana.Jueves: jueves.Remove(infoClase); break;
            case DiaSemana.Viernes: viernes.Remove(infoClase); break;
        }

    }

}


