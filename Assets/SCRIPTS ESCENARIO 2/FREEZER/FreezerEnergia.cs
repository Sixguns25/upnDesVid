using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezerEnergia : MonoBehaviour
{
    private AudioSource audioSource;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4);

        audioSource = GetComponent<AudioSource>();

        audioSource.spatialBlend = 1.0f;
        audioSource.minDistance = 20f;    // Distancia mínima donde el sonido se escucha a máximo volumen
        audioSource.maxDistance = 150f;   // Distancia máxima donde el sonido ya no se escucha
        audioSource.rolloffMode = AudioRolloffMode.Linear;// Modo en el que el sonido decae con la distancia
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void setDirectionEnergiaFreezer(string direccion)
    {
        rb = GetComponent<Rigidbody2D>();
        if (direccion == "Izquierda")
        {
            rb.velocity = new Vector2(-25, 0);
        }
        if (direccion == "Derecha")
        {
            rb.velocity = new Vector2(25, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Goku")
        {
            collision.gameObject.GetComponent<VPerderVidas>().perderVida();
            Destroy(gameObject);
        }
    }
}


