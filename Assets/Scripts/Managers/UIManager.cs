using System.Collections;
using System.Collections.Generic;
using TMPro;                        //pendiente aqui....
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Textos")]
    [SerializeField] private TextMeshProUGUI diamantesObtenidosTMP;
    [SerializeField] private TextMeshProUGUI puntajeTMP;

    private void Update()
    {
        diamantesObtenidosTMP.text = GameManager.Instancia.MonedasObtenidasEnEsteNivel.ToString();
        puntajeTMP.text = GameManager.Instancia.Puntaje.ToString();
    }

}


