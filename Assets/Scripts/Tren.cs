using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tren : MonoBehaviour
{

    [SerializeField] private float velocidad;

    public bool PuedeMoverse { get; set; }
    public PlayerController Player { get; set; }

    // Update is called once per frame
    void Update()
    {
        if (PuedeMoverse)
        {
            transform.Translate(Vector3.forward * -velocidad * Time.deltaTime);
            if (transform.position.z + 40 < Player.transform.position.z)
            {
                PuedeMoverse = false;
            }
        }
    }
}
