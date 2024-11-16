using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiempoDeVida : MonoBehaviour
{
    [SerializeField] private float tiempoDeVida;

    [SerializeField] private AudioClip sonidoExplosion; // Clip de audio para el sonido
    private void Start()
    {
        ControladorSonido.Instance.EjecutarSonido(sonidoExplosion);
        Destroy(gameObject, tiempoDeVida);
    }
}
