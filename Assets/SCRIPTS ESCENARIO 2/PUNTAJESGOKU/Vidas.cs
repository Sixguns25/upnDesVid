using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vidas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Goku"|| collision.gameObject.tag == "GokuSSJDIOS")
        {
            collision.gameObject.GetComponent<VPerderVidas>().perderVida();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Goku" || collision.gameObject.tag == "GokuSSJDIOS")
        {
            collision.gameObject.GetComponent<VPerderVidas>().perderVida();
        }
    }
}
