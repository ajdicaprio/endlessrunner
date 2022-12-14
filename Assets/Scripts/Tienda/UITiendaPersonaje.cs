using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UITiendaPersonaje : MonoBehaviour
{
    [Header("UI Personajes")]
    [SerializeField] private PersonajeCard[] cards;
    [SerializeField] private GameObject buttonComprar;
    [SerializeField] private GameObject buttonSeleccionar;
    [SerializeField] private GameObject textoSeleccionado;
    [SerializeField] private TextMeshProUGUI costoPersonajeTMP;


    [Header("UI Personajes")]
    [SerializeField] private GameObject[] personajes;
    [SerializeField] private TextMeshProUGUI textoDiamantesTotales;  //using TMPro;
    [SerializeField] private RawImage fondoImagen; //using UnityEngine.UI;
    [SerializeField] private float xValor;
    [SerializeField] private float yValor;

    private PersonajeCard cardClickeado;
    private PersonajeCard cardCargado;

    // Start is called before the first frame update
    void Start()
    {
        cardCargado = cards[PersonajeManager.Instancia.PersonajeSeleccionadoIndex];
        ActualizarInfo(cardCargado);
    }

    // Update is called once per frame
    void Update()
    {
        textoDiamantesTotales.text = MonedaManager.Instancia.MonedasTotales.ToString();

        fondoImagen.uvRect = new Rect(fondoImagen.uvRect.position
                                      + new Vector2(xValor, yValor) * Time.deltaTime,
                                      fondoImagen.uvRect.size);                                   
    }

    public void ComprarPersonaje()
    {
        if (MonedaManager.Instancia.MonedasTotales >= cardClickeado.Costo)
        {
            cardClickeado.ComprarPersonaje();
            ActualizarInfo(cardClickeado);
            MonedaManager.Instancia.GastarMonedas(cardClickeado.Costo);
        }
    }

    public void SeleccionarPersonaje()
    {
        for (int i = 0; i < cards.Length; i++)
        {
            cards[i].DeseleccionarPersonaje();

        }
        PersonajeManager.Instancia.SeleccionarPersonaje(cardClickeado);
        cardClickeado.SeleccionarPersonaje();
        ActualizarInfo(cardClickeado);
    }

    public void RegresarAlMenu() //lo referencio es las funciones clic del boton en el inspector
    {
        SceneManager.LoadScene("EndlessRunner"); //using UnityEngine.SceneManagement;
    }

    private void ActualizarInfo(PersonajeCard card) //solo llama los metodos que se reutilizan varias veces
    {
        MostrarUISegunCondicion(card);
        MostrarPersonajeSeleccionado(card);

    }

    private void MostrarUISegunCondicion(PersonajeCard card)
    {
        if (card.Comprado)
        {
            if (card.Seleccionado)
            {
                buttonComprar.SetActive(false);
                buttonSeleccionar.SetActive(false);
                textoSeleccionado.SetActive(true);
            }
            else
            {
                buttonComprar.SetActive(false);
                buttonSeleccionar.SetActive(true);
                textoSeleccionado.SetActive(false);
            }
        }
        else
        {
            costoPersonajeTMP.text = card.Costo.ToString();
            buttonComprar.SetActive(true);
            buttonSeleccionar.SetActive(false);
            textoSeleccionado.SetActive(false);
        }
    }

    private void MostrarPersonajeSeleccionado(PersonajeCard card)
    {
        for (int i = 0; i < personajes.Length; i++)
        {
            personajes[i].SetActive(false);
        }
        personajes[card.Index].SetActive(true);

    }


    //SUSCRIPCION AL EVENTO ClickCard

    private void RespuestaEventoClickCard(PersonajeCard card)
    {
        cardClickeado = card;
        ActualizarInfo(card);
    }

    private void OnEnable()
    {
        PersonajeCard.EventoClickCard += RespuestaEventoClickCard;
    }

    private void OnDisable()
    {
        PersonajeCard.EventoClickCard -= RespuestaEventoClickCard;
    }
}
