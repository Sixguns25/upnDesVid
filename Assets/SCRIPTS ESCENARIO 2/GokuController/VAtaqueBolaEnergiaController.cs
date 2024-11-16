using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class VAtaqueBolaEnergiaController : MonoBehaviour
{
    public GameObject bolaEnergiaPrefab;

    private ProtagonistaController protagonistaController; // Referencia autom�tica al controlador del protagonista
    private SpriteRenderer sr;

    //SONIDOS
    public AudioClip lanzarEnergia;
    private AudioSource audioSource;

    void Start()
    {
        // Asignaci�n autom�tica de ProtagonistaController
        protagonistaController = GetComponentInParent<ProtagonistaController>();

        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Verifica que las condiciones de estado se cumplan y que haya pasado el tiempo m�nimo entre ataques
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (protagonistaController != null &&
                !protagonistaController.rebotando &&
                !protagonistaController.ejecutandoTransformacionSSJDIOS &&
                !protagonistaController.ejecutandoGolpe
                &&protagonistaController.enSuelo
                &&!protagonistaController.ejecutandoKamehameha)
            {

                // Reproducimos el sonido
                audioSource.PlayOneShot(lanzarEnergia);

                // Calculamos la posici�n de la bola de energ�a
                Vector3 posicionBolaEnergia = transform.position;
                posicionBolaEnergia.x += sr.flipX ? -3f : 3f;

                // Instanciamos la bola de energ�a en la posici�n calculada
                GameObject energia = Instantiate(bolaEnergiaPrefab, posicionBolaEnergia, Quaternion.identity);
                energia.GetComponent<VEnergiaController>().SetDirection(sr.flipX ? "left" : "right");
            }
        }
    }
}
