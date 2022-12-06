using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamante : MonoBehaviour
{
    [SerializeField] private int valorMonedas = 1;

    private void ObtenerDiamante()
    {
        //aumentar las monedas
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ObtenerDiamante();
        }    
    }
}
