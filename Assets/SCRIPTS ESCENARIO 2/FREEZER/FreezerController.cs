using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public class FreezerController : MonoBehaviour
{
    // ANIMACIONES
    private const int QUIETO = 0;
    private const int DESPLAZARSE = 1;
    private const int GOLPE = 2;
    private const int LANZARPODER = 3;

    public Transform objetivo;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject EnergiaFreezerPrefab;

    public float speed = 20f;
    private float distancia;

    public float distanciaAbsoluta;

    public float disparo;
    public float tiempoDisparo = 2f;

    private string IzquierdaODerecha;

    private bool puedeGolpear = true; // Controla el intervalo entre golpes
    private bool enAnimacionDeGolpe = false; // Indica si la animaci n de golpe est  en curso

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        disparo = tiempoDisparo;
    }

    // Update is called once per frame
    void Update()
    {
        // Verificar si la animaci n de golpe est  en curso
        enAnimacionDeGolpe = animator.GetCurrentAnimatorStateInfo(0).IsName("Golpe");

        // Evitar otras acciones si est  en animaci n de golpe
        if (enAnimacionDeGolpe) return;

        distancia = objetivo.position.x - transform.position.x;
        distanciaAbsoluta = Mathf.Abs(distancia);

        // Movimiento hacia el objetivo
        if (distanciaAbsoluta < 25 && distanciaAbsoluta >= 9)
        {
            transform.position = Vector2.MoveTowards(transform.position, objetivo.position, speed * Time.deltaTime);
            animator.SetInteger("Estado", DESPLAZARSE);
        }
        else
        {
            animator.SetInteger("Estado", QUIETO);
        }

        // Direcci n del personaje
        if (distancia > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            IzquierdaODerecha = "Derecha";
        }
        else if (distancia < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            IzquierdaODerecha = "Izquierda";
        }

        // Lanzar poder si est  lejos
        disparo -= Time.deltaTime;
        if (distanciaAbsoluta > 30 && disparo <= 0)
        {
            animator.SetInteger("Estado", LANZARPODER);
        }

        // Golpear si est  cerca
        if (distanciaAbsoluta < 9 && puedeGolpear)
        {
            StartCoroutine(Golpear());
        }
    }

    private IEnumerator Golpear()
    {
        puedeGolpear = false;
        animator.SetInteger("Estado", GOLPE);

        // Esperar a que termine la animaci n (aseg rate de que dure 1 segundo o ajusta este tiempo)
        yield return new WaitForSeconds(1f);

        // Regresar al estado QUIETO o el que prefieras
        animator.SetInteger("Estado", QUIETO);
        puedeGolpear = true;
    }

    public void DispararEnergiaFreezer()
    {
        Vector3 posicionBolaEnergia = transform.position;

        if (IzquierdaODerecha == "Derecha")
        {
            posicionBolaEnergia.x += 4;
        }
        else
        {
            posicionBolaEnergia.x -= 4;
        }

        GameObject bolaEnergia = Instantiate(EnergiaFreezerPrefab, posicionBolaEnergia, quaternion.identity);
        bolaEnergia.GetComponent<FreezerEnergia>().setDirectionEnergiaFreezer(IzquierdaODerecha);

        disparo = tiempoDisparo;
    }
}