using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMovimiento : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private float alturaDelPersonaje;
    [SerializeField] private float distanciaDelPersonaje;

    private Vector3 miCamaraPos;

    // Start is called before the first frame update
    void Start()
    {
        miCamaraPos = transform.position;
    }

    // Se recomienda usar LateUpdate para la posicion de la camara
    void LateUpdate()
    {
        Vector3 posPersonaje = GameManager.Instancia.PersonajeActivo.position;
        miCamaraPos.x = Mathf.Lerp(miCamaraPos.x, posPersonaje.x, Time.deltaTime * 10f);
        miCamaraPos.y = Mathf.Lerp(miCamaraPos.y, posPersonaje.y + alturaDelPersonaje, Time.deltaTime * 10f);
        miCamaraPos.z = Mathf.Lerp(miCamaraPos.z, posPersonaje.z + distanciaDelPersonaje, Time.deltaTime * 10f);
        transform.position = miCamaraPos;
    }
}
