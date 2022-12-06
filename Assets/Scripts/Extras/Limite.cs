using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limite : MonoBehaviour
{

    public static event Action EventoSolicitudNuevoBloque;  //using System
     

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bloque"))
        {
            //if (EventoSolicitudNuevoBloque != null)
            //{
            //    EventoSolicitudNuevoBloque.Invoke();
            //}  
            EventoSolicitudNuevoBloque?.Invoke(); //esto reemplaza lo de arriba



            other.transform.position = Vector3.zero;
            other.gameObject.SetActive(false);
        }
    }
}
