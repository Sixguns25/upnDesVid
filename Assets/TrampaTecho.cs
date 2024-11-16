using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampaTecho : MonoBehaviour
{
    public Rigidbody2D rb2D;
    public float distanciaLinea;
    public LayerMask capaJugador;
    public bool estaSubiendo = false;
    public float velocidadSubida;
    public float tiempoEspera;
    public Animator animator;

    [SerializeField] private AudioClip sonidoGolpe; // Clip de audio para el sonido de golpe

    private void Update()
    {
        RaycastHit2D infoJugador = Physics2D.Raycast(transform.position, Vector3.down, distanciaLinea, capaJugador);

        if (infoJugador)
        {
            rb2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
        if (estaSubiendo)
        {
            transform.Translate(Time.deltaTime * velocidadSubida * Vector3.up);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("suelo")) {
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll;
            ControladorSonido.Instance.EjecutarSonido(sonidoGolpe);
            if (estaSubiendo) {
                estaSubiendo = false;
            }
            else
            {
                animator.SetTrigger("Golpe");
                estaSubiendo=true;
            }
        }

        if (collision.gameObject.TryGetComponent(out DestruirJugador destruirJugador)) 
        {
            destruirJugador.TomarDaño();
        }
    }
    private IEnumerator EsperarEnSuelo()
    {
        yield return new  WaitForSeconds(tiempoEspera);
        estaSubiendo = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * distanciaLinea);
    }
}
