using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VidaJugador : MonoBehaviour
{
    //public int cantidadDeVida;

    //public void TomarDaño(int daño)
    //{
    //    cantidadDeVida -= daño;
    //    if(cantidadDeVida < 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    public int vidaActual;

    public int vidaMaxima;

    public UnityEvent<int> cambioVida;

    private void Start()
    {
        vidaActual = vidaMaxima;
        cambioVida.Invoke(vidaActual);
    }
    private void Update()
    {
        
    }
    public void TomarDaño(int cantidadDaño)
    {
        int vidaTemporal = vidaActual - cantidadDaño;
        if (vidaTemporal < 0)
        {
            vidaActual = 0;
        }
        else
        {
            vidaActual = vidaTemporal;
        }

        cambioVida.Invoke(vidaActual);

        if (vidaActual <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void CurarVIda(int cantidadCuracion)
    {
        int vidaTemporal = vidaActual + cantidadCuracion;

        if (vidaTemporal > vidaMaxima)
        {
            vidaActual = vidaMaxima;
        }
        else
        {
            vidaActual = vidaTemporal;
        }
        cambioVida.Invoke(vidaActual);
    }
}
