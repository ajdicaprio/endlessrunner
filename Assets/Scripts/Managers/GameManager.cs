using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadosDelJuego
{
    Inicio,
    Jugando,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    public static event Action EventoImanFinalizado;

    [SerializeField] private int velocidadMundo = 5;
    [SerializeField] private int multiplicadorPuntajePorMonedad = 10;

    public int Puntaje => (int) distanciaRecorrida + MonedasObtenidasEnEsteNivel * multiplicadorPuntajePorMonedad;

    public float ValorMultiplicador { get; set; }

    public EstadosDelJuego EstadoActual { get; set; } //prop
    public int MonedasObtenidasEnEsteNivel { get; set; } //prop

    private float distanciaRecorrida;

    private void Start()
    {
        ValorMultiplicador = 1f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CambiarEstado(EstadosDelJuego.Jugando);
        }

        if (EstadoActual == EstadosDelJuego.Inicio || EstadoActual == EstadosDelJuego.GameOver)
        {
            return;
        }
        distanciaRecorrida += Time.deltaTime * velocidadMundo * ValorMultiplicador;
    }

    public void CambiarEstado(EstadosDelJuego nuevoEstado)
    {
        if (EstadoActual != nuevoEstado)
        {
            EstadoActual = nuevoEstado;
        }
    }

    public void IniciarConteoMultiplicador(float tiempo)
    {
        StartCoroutine(COMultiplicadorConteo(tiempo));
    }

    private IEnumerator COMultiplicadorConteo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        ValorMultiplicador = 1;
    }

    private IEnumerator COImanConteo(float tiempo)
    {
        yield return new WaitForSeconds(tiempo);
        EventoImanFinalizado?.Invoke();
        
    }

    private void RespuestaEventoIman(float duracion)
    {
        StartCoroutine(COImanConteo(duracion));
    }

    private void OnEnable()
    {
        PotenciadorIman.EventoIman += RespuestaEventoIman;
    }

    private void OnDisable()
    {
        PotenciadorIman.EventoIman -= RespuestaEventoIman;
    }
}
