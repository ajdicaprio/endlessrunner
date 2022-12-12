using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaManager : Singleton<MonedaManager>
{

    public int MonedasTotales { get; private set; }
    private string MONEDAS_KEY = "MIS_MONEDAS";

    protected override void Awake()
    {
        base.Awake();
        MonedasTotales = PlayerPrefs.GetInt(MONEDAS_KEY);
    }

    public void AnadirMonedas(int cantidad)
    {
        MonedasTotales += cantidad;
        PlayerPrefs.SetInt(MONEDAS_KEY, MonedasTotales);
        PlayerPrefs.Save();
    }

    public void GastarMonedas(int cantidad)
    {
        if (MonedasTotales >= cantidad)
        {
            MonedasTotales -= cantidad;
            PlayerPrefs.SetInt(MONEDAS_KEY, MonedasTotales);
            PlayerPrefs.Save();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            AnadirMonedas(1);
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            GastarMonedas(1);
        }

    }
}
