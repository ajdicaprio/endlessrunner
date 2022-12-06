using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EstadosDelJuego
{
    Inicio,
    Jugando,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public EstadosDelJuego EstadoActual { get; set; } //prop

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CambiarEstado(EstadosDelJuego.Jugando);
        }
    }

    public void CambiarEstado(EstadosDelJuego nuevoEstado)
    {
        if (EstadoActual != nuevoEstado)
        {
            EstadoActual = nuevoEstado;
        }
    } 
}
