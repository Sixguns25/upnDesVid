using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtagonistaController : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator animator;
    private SpriteRenderer sr;

    public float fuerzaSalto = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;

    public bool enSuelo;
    public bool rebotando; //Variable de estado para el rebote temporal

    //VARIABLES PARA CONTROLAR LAS ANIMACIONES
    public bool ejecutandoTransformacionSSJDIOS = false; //Controlar animacion de transformacion
    public bool ejecutandoGolpe = false;

    //SONIDOS
    public AudioClip jumpSound;
    public AudioClip golpeSound;
    public AudioClip TRANSFORMSSJDIOS;
    private AudioSource audioSource;

    //TRANSFORMACIONES
    [Header("TRANSFORMACIONES")]
    [SerializeField] private AnimatorOverrideController GOKUSSJDIOS;

    private RuntimeAnimatorController GOKUBASE;

    //ESTA TRANSFORMADO?
    private bool EstaEnSSJDIOS=false;

    //PARA CONTROLAR EL KAMEHAMEHA
    public bool ejecutandoKamehameha = false;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        GOKUBASE=animator.runtimeAnimatorController;
        sr = GetComponent<SpriteRenderer>();
        rebotando = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (ejecutandoKamehameha)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        if (!rebotando && !ejecutandoTransformacionSSJDIOS && !ejecutandoGolpe && !ejecutandoKamehameha)
        {
            animator.SetInteger("Estado", 0);
            rb.velocity = new Vector2(0, rb.velocity.y);

            if (Input.GetKey(KeyCode.D))
            {
                rb.velocity = new Vector2(25, rb.velocity.y);
                sr.flipX = false;
            }
            if (Input.GetKey(KeyCode.A))
            {
                rb.velocity = new Vector2(-25, rb.velocity.y);
                sr.flipX = true;
            }

            if (rb.velocity.x != 0)
            {
                animator.SetInteger("Estado", 1);
            }


            if (Input.GetKeyDown(KeyCode.J) && enSuelo)
            {
                StartCoroutine(ANIMGOLPE());
                if (sr.flipX)
                {
                    rb.velocity = new Vector2(-10, rb.velocity.y);
                }
                else
                {
                    rb.velocity = new Vector2(10, rb.velocity.y);
                }
                

            }

            if (Input.GetKey(KeyCode.U)&& enSuelo)
            {
                animator.SetInteger("Estado", 4);
                
            }

            if (Input.GetKeyDown(KeyCode.L) && !EstaEnSSJDIOS) 
            {
                StartCoroutine(ANIMTRANSFORMSSJDIOS()); // Ejecuta Estado 6 como una corrutina
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                animator.runtimeAnimatorController = GOKUBASE as RuntimeAnimatorController;
                EstaEnSSJDIOS=false;
            }

        }

        if (rebotando)
        {
            animator.SetInteger("Estado",5);
        }
           

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
        enSuelo = hit.collider != null;

        if (enSuelo && Input.GetKeyDown(KeyCode.K) &&!ejecutandoKamehameha)
        {
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            animator.SetInteger("Estado", 2);
            audioSource.PlayOneShot(jumpSound);
        }
        if (!enSuelo && !rebotando)
        {
            animator.SetInteger("Estado", 2);
        }
    }

    private IEnumerator ANIMTRANSFORMSSJDIOS()
    {
        ejecutandoTransformacionSSJDIOS = true; // Activar el bloqueo
        rb.velocity = new Vector2(0, rb.velocity.y);
        animator.SetInteger("Estado", 6);
        audioSource.PlayOneShot(TRANSFORMSSJDIOS);

        // Esperar hasta que el Animator esté en el estado 6
        yield return null;
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("TRANSFORMACIONSSJDIOS"))
        {
            yield return null;
        }

        // Una vez que estamos en el estado correcto, esperamos a que termine
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        ejecutandoTransformacionSSJDIOS = false; // Desactivar el bloqueo
        EstaEnSSJDIOS=true;
    }

    private IEnumerator ANIMGOLPE()
    {
        ejecutandoGolpe=true;
        animator.SetInteger("Estado", 3);
        audioSource.PlayOneShot(golpeSound);

        // Esperar hasta que el Animator esté en el estado 3
        yield return null;
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Golpe1"))
        {
            yield return null;
        }

        // Una vez que estamos en el estado correcto, esperamos a que termine
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        ejecutandoGolpe = false;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaDinamica1")
        {
            transform.parent = collision.transform;
        }

        if (collision.gameObject.tag == "Enemigo" || collision.gameObject.tag == "EnemigoAgua" || collision.gameObject.tag == "EnemigoVolador" && rebotando==true)
        {
            float direction = transform.position.x - collision.transform.position.x;
            float fuerzaHorizontal = direction > 0 ? 12f : -12f;

            // Establece el estado de rebote y aplica la fuerza
            rebotando = true;
            rb.AddForce(new Vector2(fuerzaHorizontal, fuerzaSalto), ForceMode2D.Impulse);

            // Restaura el control horizontal despu s de un breve per odo
            Invoke(nameof(FinRebote), 0.8f);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaDinamica1")
        {
            transform.parent = null;
        }
    }

    // Funci n para restaurar el control horizontal
    private void FinRebote()
    {
        rebotando = false;
    }

    private void TransformarSSJDIOS()
    {
        animator.runtimeAnimatorController = GOKUSSJDIOS as RuntimeAnimatorController;
    }

}
