using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component 
{
    //darle posibilidad a las clases para que no se destruyan en cambio de escena
    [SerializeField] private bool activarDontDestroy;

    private static T _instancia;
    public static T Instancia
    {
        get
        {
            if (_instancia == null)
            {
                _instancia = FindObjectOfType<T>();
                if (_instancia == null)
                {
                    GameObject nuevoGO = new GameObject();
                    _instancia = nuevoGO.AddComponent<T>();
                }
            }

            return _instancia;
        }
    }

    protected virtual void Awake()
    {
        if (activarDontDestroy)
        {
            if (_instancia == null)
            {
                _instancia = this as T;
            }
            else
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            _instancia = this as T;

        }


    }
}