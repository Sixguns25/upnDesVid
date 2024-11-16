using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoDaño : MonoBehaviour
{
    public int daño;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out VidaJugador vidaJugador))
        {
            vidaJugador.TomarDaño(daño);
        }
    }
}
