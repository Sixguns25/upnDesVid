using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoCurar : MonoBehaviour
{
    public int curacion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out VidaJugador vidaJugador))
        {
            vidaJugador.CurarVIda(curacion);
            Destroy(gameObject);
        }
    }
}
