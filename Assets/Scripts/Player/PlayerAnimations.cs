using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{

    //Para actualizar las animaciones debemos obtener una referencia del componente Animator
    //dentro del GameObjetc del personaje y saber que animacion estamos reproducioendo actual
    //para poder reproducir una nueva
    //Borramos Start y Update

    private Animator animator;
    private string animacionActual;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void CambiarAnimacion(string nuevaAnimacion)
    {
        if (animacionActual == nuevaAnimacion)
        {
            return;
        }

        animator.Play(nuevaAnimacion);
        animacionActual = nuevaAnimacion;
    }

    public void MostrarAnimacionIdle()
    {
        CambiarAnimacion("Idle");
    }
    public void MostrarAnimacionCorrer()
    {
        CambiarAnimacion("Run");
    }
    public void MostrarAnimacionSaltar()
    {
        CambiarAnimacion("Jump");
    }
    public void MostrarAnimacionDeslizar()
    {
        CambiarAnimacion("Crawl");
    }
    public void MostrarAnimacionColision()
    {
        CambiarAnimacion("Dead");
    }


}
