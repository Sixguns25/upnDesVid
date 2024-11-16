using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VBolaFuegoEnemigoVolador : MonoBehaviour
{
    public float defaultVelocityY = -10;
    Rigidbody2D rb;
    Animator anim;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 3);
        anim=GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        audioSource.spatialBlend = 1.0f;
        audioSource.minDistance = 20f;    // Distancia mínima donde el sonido se escucha a máximo volumen
        audioSource.maxDistance = 150f;   // Distancia máxima donde el sonido ya no se escucha
        audioSource.rolloffMode = AudioRolloffMode.Linear;// Modo en el que el sonido decae con la distancia
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(0, defaultVelocityY);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Goku")
    //    {
    //        collision.gameObject.GetComponent<VPerderVidas>().perderVida();
    //        //anim.SetInteger("Estado",1);
    //        //defaultVelocityY = 0;
    //        Destroy(gameObject);
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Goku")
        {
            collision.gameObject.GetComponent<VPerderVidas>().perderVida();
            //anim.SetInteger("Estado",1);
            //defaultVelocityY = 0;
            Destroy(gameObject);
        }
    }

    public void EliminarBolaFuego()
    {
        Destroy(gameObject);
    }
}
