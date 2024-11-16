using System.Collections;
using UnityEngine;

public class VKamehamehaController : MonoBehaviour
{
    public GameObject KamehaPrefab;
    private SpriteRenderer sr;
    //SONIDOS
    public AudioClip CargarKameha;
    public AudioClip LanzarKameha;
    private AudioSource audioSource;


    private Animator animator;
    private ProtagonistaController protagonistaController;
    private bool cargando;
    private bool disparando;

    void Start()
    {
        animator = GetComponent<Animator>();
        protagonistaController = GetComponent<ProtagonistaController>();
        cargando = false;
        disparando = false;

        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I) && !cargando && !disparando)
        {
            cargando = true;
            protagonistaController.ejecutandoKamehameha = true; // Inicia el bloqueo en ProtagonistaController
            animator.SetInteger("Estado",7);
            StartCoroutine(VerificarCargaLoop());
        }

        if (Input.GetKeyUp(KeyCode.I) && cargando)
        {
            cargando = false;
            disparando = true;
            animator.SetInteger("Estado",9);

            //SONIDO LANZAR KAMEHAMEHA
            audioSource.clip = LanzarKameha;
            audioSource.Play();

            // Calculamos la posición de la bola de energía
            Vector3 posicionKameha = transform.position;
            posicionKameha.x += sr.flipX ? -30f : 30f;

            // Instanciamos la bola de energía en la posición calculada
            GameObject kameha = Instantiate(KamehaPrefab, posicionKameha, Quaternion.identity,transform);
            kameha.GetComponent<VKamePrefab>().SetDirection(sr.flipX ? "left" : "right");
            StartCoroutine(FinalizarDisparo());
        }
    }

    private IEnumerator VerificarCargaLoop()
    {
        //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        yield return new WaitForSeconds(0.05f);
        // Iniciar el sonido de carga en bucle
        audioSource.clip = CargarKameha;
        audioSource.loop = true;
        audioSource.Play();

        while (Input.GetKey(KeyCode.I))
        {
            animator.SetInteger("Estado",8);
            //yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            yield return new WaitForSeconds(0.05f); // Tiempo para el bucle de carga
        }
        // Detener el sonido de carga cuando se suelte la tecla
       // audioSource.Stop();
        audioSource.loop = false;
    }

    private IEnumerator FinalizarDisparo()
    {
        //audioSource.PlayOneShot(LanzarKameha);
        yield return new WaitForSeconds(4.0f);
        audioSource.Stop();
        disparando = false;
        protagonistaController.ejecutandoKamehameha = false; // Quita el bloqueo en ProtagonistaController
    }
}
