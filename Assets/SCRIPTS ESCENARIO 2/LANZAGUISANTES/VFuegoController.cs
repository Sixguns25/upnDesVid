using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFuegoController : MonoBehaviour
{
    public float defaultVelocityX = -30;


    Rigidbody2D rb;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 5);

        audioSource = GetComponent<AudioSource>();

        audioSource.spatialBlend = 1.0f; 
        audioSource.minDistance = 20f;    // Distancia m�nima donde el sonido se escucha a m�ximo volumen
        audioSource.maxDistance = 150f;   // Distancia m�xima donde el sonido ya no se escucha
        audioSource.rolloffMode = AudioRolloffMode.Linear;// Modo en el que el sonido decae con la distancia
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(defaultVelocityX, rb.velocity.y);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Goku")
    //    {
    //        collision.gameObject.GetComponent<PerderVidas>().perderVida();
    //        Destroy(gameObject);
    //    }
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Goku")
        {
            collision.gameObject.GetComponent<VPerderVidas>().perderVida();
            Destroy(gameObject);
        }
    }
}
