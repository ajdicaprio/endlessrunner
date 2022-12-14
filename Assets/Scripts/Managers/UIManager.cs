using System.Collections;
using System.Collections.Generic;
using TMPro;                        //pendiente aqui....
using UnityEngine;
using UnityEngine.SceneManagement;  //y aqui...

public class UIManager : MonoBehaviour
{
    [Header("Paneles")]
    [SerializeField] private GameObject panelMenuInicio;
    [SerializeField] private GameObject panelMenuGameOver;


    [Header("Textos")]
    [SerializeField] private TextMeshProUGUI diamantesObtenidosTMP;
    [SerializeField] private TextMeshProUGUI diamantesTotalesTMP;
    [SerializeField] private TextMeshProUGUI diamantesGameOverTMP;
    [SerializeField] private TextMeshProUGUI puntajeTMP;
    [SerializeField] private TextMeshProUGUI mejorPuntajeTMP;
    [SerializeField] private TextMeshProUGUI puntajeGameOverTMP;

    private void Start()
    {
        ActualizarMenu();

    }

    private void Update()
    {
        diamantesObtenidosTMP.text = GameManager.Instancia.MonedasObtenidasEnEsteNivel.ToString();
        puntajeTMP.text = GameManager.Instancia.Puntaje.ToString();
    }

    public void TiendaPersonajes()
    {
        SceneManager.LoadScene("TiendaPersonajes");
    }

    private void ActualizarMenu()
    {
        diamantesTotalesTMP.text = MonedaManager.Instancia.MonedasTotales.ToString();
        mejorPuntajeTMP.text = GameManager.Instancia.MejorPuntaje.ToString();
    }

    private void ActualizarMenuGameOver()
    {
        panelMenuGameOver.SetActive(true);
        puntajeGameOverTMP.text = GameManager.Instancia.Puntaje.ToString();
        diamantesGameOverTMP.text = GameManager.Instancia.MonedasObtenidasEnEsteNivel.ToString();
    }

    public void Jugar()
    {
        panelMenuInicio.SetActive(false);
        GameManager.Instancia.CambiarEstado(EstadosDelJuego.Jugando);
    }

    public void Reintentar()
        //debe estar en publico para que lo encuentre el inspector
        //Cuendo lo elijo en el inspector se colorea como usado
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //using UnityEngine.SceneManagement;
    }

    private void RespuestaEventoCambioDeEstado(EstadosDelJuego nuevoEstado)
    {
        if (nuevoEstado == EstadosDelJuego.GameOver)
        {
            ActualizarMenuGameOver();
        }
    }

    private void OnEnable()
    {
        GameManager.EventoCambioDeEstado += RespuestaEventoCambioDeEstado;
    }

    private void OnDisable()
    {
        GameManager.EventoCambioDeEstado -= RespuestaEventoCambioDeEstado;

    }

}


