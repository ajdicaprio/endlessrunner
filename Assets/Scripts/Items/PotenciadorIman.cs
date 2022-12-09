using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotenciadorIman : MonoBehaviour 
{
    //Evento
    public static event Action<float> EventoIman;
    //Action necesita using.System
    //En Diamante me suscribo al evento con on enable y on disable

    [SerializeField] private float duracion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            EventoIman?.Invoke(duracion);
            gameObject.SetActive(false);
        }
    }
}
