using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidad : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float da�o;


    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    public void AumentarDa�o(int da�oExtra)
    {
        da�o += da�oExtra * da�o;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            collision.GetComponent<Enemigo>().TomarDa�o(da�o);
            Debug.Log("Da�o: " + da�o);
            Destroy(gameObject);
        }
    }
}
