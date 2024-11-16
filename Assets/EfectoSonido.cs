using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{

    [SerializeField] private AudioClip colectar1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hero"))
        {
            ControladorSonido.Instance.EjecutarSonido(colectar1);
            Destroy(gameObject);

        }
    }
}
