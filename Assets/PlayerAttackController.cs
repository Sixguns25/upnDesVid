using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Animator animator; // A�ade referencia al Animator

    SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); // Obt�n el componente Animator del personaje
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            // Activa el bool para lanzar la habilidad
            animator.SetBool("lanzandoHabilidad", true);

            // Iniciar la corutina que espera la duraci�n de la animaci�n para lanzar la bola de fuego
            StartCoroutine(LanzarHabilidadDespuesDeAnimacion());
        }
    }

    IEnumerator LanzarHabilidadDespuesDeAnimacion()
    {
        yield return new WaitForSeconds(1.4f); // Duraci�n de la animaci�n

        // Instanciar la bola de fuego y pasar la direcci�n seg�n el personaje est� volteado o no
        GameObject FireBall = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        // Pasamos la direcci�n seg�n si el personaje est� mirando a la izquierda o la derecha
        FireBall.GetComponent<FireBallController>().SetDirection(sr.flipX ? "left" : "right");

        // Desactiva el bool despu�s de lanzar la bola de fuego
        animator.SetBool("lanzandoHabilidad", false);
    }

}
