using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaManager : MonoBehaviour
    //en el curso lo hace con singleton, hasta ahora funciona sin singleton
{

    public int MonedasTotales { get; private set; }
    private string MONEDAS_KEY = "MIS_MONEDAS";

    private void Awake()
    {
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
