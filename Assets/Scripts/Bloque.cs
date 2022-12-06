using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TipoBloque
{
    Normal,
    Full,
    Trenes
}

public class Bloque : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TipoBloque tipoBloque;
    [SerializeField] private bool tieneRampa; //probar aqui poner public y no usar propiedades

    [Header("Tren")]
    [SerializeField] private Tren[] trenes;

    private Tren trenSeleccionado;

    //propiedades que regresan valores porque la variable arriba es privada. Y si la pongo publica?
    public TipoBloque TipoDeBloque => tipoBloque;
    public bool TieneRampa => tieneRampa;


    public void InicializarBloque()
    {
        if (tipoBloque == TipoBloque.Trenes)
        {
            SeleccionarTren();
        }
    }

    public void SeleccionarTren()
    {
        if (trenes == null || trenes.Length == 0)
        {
            return;
        }

        int index = Random.Range(0, trenes.Length);
        trenes[index].gameObject.SetActive(true);
        trenSeleccionado = trenes[index];

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (trenSeleccionado != null)
            {
                trenSeleccionado.PuedeMoverse = true;
                trenSeleccionado.Player = other.GetComponent<PlayerController>();
            }
        }
    }


}
