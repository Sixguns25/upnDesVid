using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habilidad : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private float daño;


    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    public void AumentarDaño(int dañoExtra)
    {
        daño += dañoExtra * daño;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemigo"))
        {
            collision.GetComponent<Enemigo>().TomarDaño(daño);
            Debug.Log("Daño: " + daño);
            Destroy(gameObject);
        }
    }
}
