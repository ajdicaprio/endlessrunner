using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    [Header("Config")]
    [SerializeField] private int bloquesAlInicio = 5;
    [SerializeField] private int maxBloquesParaFull = 5;
    [SerializeField] private int maxBloquesParaTrenes = 10;
    [SerializeField] private int maxBloquesTrenesParaReset = 2;

    [Header("Bloques")]
    [SerializeField] private Bloque bloqueInicial;
    [SerializeField] private int longitudBloqueNormal = 40;
    [SerializeField] private int longitudBloqueTrenes = 80;
    [SerializeField] private Bloque[] bloquesPrefab;

    private List<Bloque> listaBloquesNormales = new List<Bloque>();
    private List<Bloque> listaBloquesFull = new List<Bloque>();
    private List<Bloque> listaBloquesTrenes = new List<Bloque>();
    private List<Bloque> listaBloquesConRampa = new List<Bloque>();

    private Pooler pooler;
    private Bloque ultimoBloque;
    private int bloquesCreados;

    private void Awake()
    {
        pooler = GetComponent<Pooler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        LlenarBloquesSegunTipo();
        ultimoBloque = bloqueInicial;
        for (int i = 0; i < bloquesAlInicio; i++)
        {
            CrearBloque();
        }
    }

    private void CrearBloque()
    {
        if (bloquesCreados >= maxBloquesParaTrenes)
        {
            if (bloquesCreados < maxBloquesParaTrenes + 1)
            {
                AnadirBloque(TipoBloque.Trenes, longitudBloqueNormal);
            }
            else
            {
                AnadirBloque(TipoBloque.Trenes, longitudBloqueTrenes);
            }

            if (bloquesCreados == maxBloquesParaTrenes + maxBloquesTrenesParaReset)
            {
                bloquesCreados = 0;
            }
        }
        else if (bloquesCreados >= maxBloquesParaFull)
        {
            AnadirBloque(TipoBloque.Full, longitudBloqueNormal);
        }
        else
        {
            if (bloquesCreados == maxBloquesParaFull - 1)
            {
                AnadirBloque(TipoBloque.Normal, longitudBloqueNormal, true);
            }
            else
            {
                if (ultimoBloque.TipoDeBloque == TipoBloque.Trenes)
                {
                    AnadirBloque(TipoBloque.Normal, longitudBloqueTrenes);
                }
                else
                {
                    AnadirBloque(TipoBloque.Normal, longitudBloqueNormal);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            // // // bloque = ObtenerBloqueSegunTipo(TipoBloque.Normal);
            // // // bloque.transform.position = bloqueInicial.transform.position + Vector3.forward * 55f;
            //
            //AnadirBloque(TipoBloque.Normal, longitudBloqueNormal);
            CrearBloque();
        }
    }

    private void AnadirBloque(TipoBloque tipo, float longitud, bool conRampa = false)
    {
        Bloque nuevoBloque = ObtenerBloqueSegunTipo(tipo, conRampa);
        nuevoBloque.transform.position = EstablecerPosicionNuevoBloque(longitud);
        ultimoBloque = nuevoBloque;
        bloquesCreados++;
    }
    

    private Bloque ObtenerBloqueSegunTipo(TipoBloque tipo, bool conRampa = false)
    {
        Bloque nuevoBloque = null;
        if (conRampa)
        {
            nuevoBloque = ObtenerInstanciaDelPooler(listaBloquesConRampa);
        }
        else
        {
            switch (tipo)
            {
                case TipoBloque.Normal:
                    nuevoBloque = ObtenerInstanciaDelPooler(listaBloquesNormales);
                    break;
                case TipoBloque.Full:
                    nuevoBloque = ObtenerInstanciaDelPooler(listaBloquesFull);
                    break;
                case TipoBloque.Trenes:
                    nuevoBloque = ObtenerInstanciaDelPooler(listaBloquesTrenes);
                    break;
            }
        }

        if (nuevoBloque != null)
        {
            nuevoBloque.InicializarBloque();
        }

        return nuevoBloque;
    }

    private Bloque ObtenerInstanciaDelPooler(List<Bloque> lista)
    {
        int bloqueRandom = Random.Range(0, lista.Count);
        string nombreDelBloque = lista[bloqueRandom].name;
        GameObject instancia = pooler.ObtenerInstanciaDelPooler(nombreDelBloque);
        instancia.SetActive(true);
        Bloque bloque = instancia.GetComponent<Bloque>();
        return bloque;

    }

    private Vector3 EstablecerPosicionNuevoBloque(float longitud)
    {
        return ultimoBloque.transform.position + Vector3.forward * longitud;
    }

    private void LlenarBloquesSegunTipo()
    {
        foreach(Bloque bloque in bloquesPrefab)
        {
            switch (bloque.TipoDeBloque)
            {
                case TipoBloque.Normal:
                    listaBloquesNormales.Add(bloque);
                    if (bloque.TieneRampa)
                    {
                        listaBloquesConRampa.Add(bloque);
                    }
                    break;
                case TipoBloque.Full:
                    listaBloquesFull.Add(bloque);
                    break;
                case TipoBloque.Trenes:
                    listaBloquesTrenes.Add(bloque);
                    break;
            }
        }
    }

    private void RespuestaSolicitudNuevoBloque()
    {
        CrearBloque();
    }

    private void OnEnable()
    {
        Limite.EventoSolicitudNuevoBloque += RespuestaSolicitudNuevoBloque;
    }

    private void OnDisable()
    {
        Limite.EventoSolicitudNuevoBloque -= RespuestaSolicitudNuevoBloque;
    }

}
