using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoJugador : MonoBehaviour
{
    [SerializeField] private Transform controladorDisparo;
    [SerializeField] private GameObject bala;

    [SerializeField] private float maximoCarga;
    [SerializeField] private float tiempoDeCarga;

    [SerializeField] private AudioClip sonidoDisparo; // Clip de audio para el sonido de disparo

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            if (tiempoDeCarga <= maximoCarga)
            {
                tiempoDeCarga += Time.deltaTime;
            }
        }

        if (Input.GetButtonUp("Fire1"))
        {
            Disparar((int)tiempoDeCarga);
            tiempoDeCarga = 0;
        }
    }
    private void Disparar(int tiempoCarga)
    {
        Vector3 crecer = new Vector3(tiempoCarga, tiempoCarga, 0);

        GameObject balaObjeto = Instantiate(bala, controladorDisparo.position, controladorDisparo.rotation);

        balaObjeto.GetComponent<Habilidad>().AumentarDaño(tiempoCarga);
        balaObjeto.transform.localScale += crecer;

        // Reproducir sonido de disparo
        ControladorSonido.Instance.EjecutarSonido(sonidoDisparo);
    }
}