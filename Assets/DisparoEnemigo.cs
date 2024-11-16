using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    public Transform controladorDisparo;
    public float distanciaLinea;
    public LayerMask capaJugador;
    public bool jugadorEnRango;

    public float tiempoEntreDisparos;
    public float tiempoUltimoDisparo;
    public GameObject balaEnemigo;
    public float tiempoEsperaDisparo;
    public Animator animator;

    [SerializeField] private AudioClip sonidoDisparo; // Clip de audio para el sonido de disparo

    private void Update()
    {
        jugadorEnRango = Physics2D.Raycast(controladorDisparo.position, transform.right, distanciaLinea, capaJugador);

        if (jugadorEnRango)
        {
            if(Time.time > tiempoEntreDisparos + tiempoUltimoDisparo)
            {
                tiempoUltimoDisparo = Time.time;
                animator.SetTrigger("Disparar");
                Invoke(nameof(Disparar), tiempoEsperaDisparo);
            }
        }
    }

    private void Disparar()
    {
        ControladorSonido.Instance.EjecutarSonido(sonidoDisparo);
        Instantiate(balaEnemigo, controladorDisparo.position, controladorDisparo.rotation);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distanciaLinea);
    }
}
