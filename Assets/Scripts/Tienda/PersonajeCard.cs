using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonajeCard : MonoBehaviour
{
    public static event Action<PersonajeCard> EventoClickCard;

    [SerializeField] private bool libre;
    [SerializeField] private int index;
    [SerializeField] private int costo;

    //las propiedades son para acceder a variables de un script desde otro script
    //las propiedades son para acceder a los valores de una clase desde otra clase
    //en este caso desde UITiendaPersonaje accedemos a PersonajeCard.Index
    public int Index => index;
    public int Costo => costo;

    //prop que devuelve una comparacion logica de one
    public bool Comprado => PlayerPrefs.GetInt(CARD_COMPRADO_KEY + index) == 1; 
    public bool Seleccionado => PlayerPrefs.GetInt(CARD_SELECCIONADO_KEY + index) == 1;

    private string CARD_COMPRADO_KEY = "COMPRADO";
    private string CARD_SELECCIONADO_KEY = "SELECCIONADO";
    private string CARD_LIBRE_KEY = "LIBRE";

    private void Awake()
    {

        //para resetear los tests.
        //PlayerPrefs.DeleteKey(CARD_COMPRADO_KEY + index);
        //PlayerPrefs.DeleteKey(CARD_SELECCIONADO_KEY + index);
        //PlayerPrefs.DeleteKey(CARD_LIBRE_KEY + index);

        if (libre)
        {
            if(PlayerPrefs.GetInt(CARD_LIBRE_KEY + index) == 0)
            {
                ComprarPersonaje();
                SeleccionarPersonaje();
                PlayerPrefs.SetInt(CARD_LIBRE_KEY + index, 1);
            }
        }
    }

    public void ClickCard()
    {
        EventoClickCard?.Invoke(this);
    }

    public void ComprarPersonaje()
    {
        PlayerPrefs.SetInt(CARD_COMPRADO_KEY + index, 1);
    }

    public void SeleccionarPersonaje()
    {
        PlayerPrefs.SetInt(CARD_SELECCIONADO_KEY + index, 1);
    }

    public void DeseleccionarPersonaje()
    {
        PlayerPrefs.SetInt(CARD_SELECCIONADO_KEY + index, 0);
    }
}
