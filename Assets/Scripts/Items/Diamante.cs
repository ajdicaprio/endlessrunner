using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamante : MonoBehaviour
{
    [SerializeField] private int valorMonedas = 1;
    [SerializeField] private float colliderNuevoTamano = 5f;
    [SerializeField] private float distanciaMinPlayer = 1.5f;
    [SerializeField] private float velocidadMovDiamantePlayer = 10f;

    public Transform Player { get; set; }

    private BoxCollider colliderDiamante;
    private Vector3 tamanoInicial;
    private bool imanActivado;

    private void Start()
    {
        colliderDiamante = GetComponent<BoxCollider>();
        tamanoInicial = colliderDiamante.size;
    }

    private void Update()
    {
        if (Player != null)
        {
            Debug.DrawLine(transform.position, Player.position, Color.blue);
            MoverDiamanteHaciaPersonaje();
        }
    }

    private void ObtenerDiamante()
    {
        //aumentar las monedas
        SoundManager.Instancia.ReproducirSonidoFX(SoundManager.Instancia.itemClip);
        MonedaManager.Instancia.AnadirMonedas(valorMonedas);
        GameManager.Instancia.MonedasObtenidasEnEsteNivel += valorMonedas;
        gameObject.SetActive(false);
    }

    private void MoverDiamanteHaciaPersonaje()
    {
        if (Vector3.Distance(Player.position, transform.position) > 0.1)
        {
            if (Vector3.Distance(Player.position, transform.position) < distanciaMinPlayer)
            {
                ObtenerDiamante();
            }

            transform.position = Vector3.MoveTowards(transform.position, Player.position + Vector3.up,
                velocidadMovDiamantePlayer * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (imanActivado)
            {
                Player = other.transform;
            }
            else
            {
                ObtenerDiamante();
            }
        }    
    }

    private void RespuestaEventoIman(float duracion)
    {
        colliderDiamante.size *= colliderNuevoTamano;
        imanActivado = true;
    }

    private void RespuestaImanFinalizado()
    {
        colliderDiamante.size = tamanoInicial;
        imanActivado = false;
    }

    private void OnEnable()
    {
        PotenciadorIman.EventoIman += RespuestaEventoIman;
        GameManager.EventoImanFinalizado += RespuestaImanFinalizado;
    }

    private void OnDisable()
    {
        PotenciadorIman.EventoIman -= RespuestaEventoIman;
        GameManager.EventoImanFinalizado -= RespuestaImanFinalizado;

    }
}
