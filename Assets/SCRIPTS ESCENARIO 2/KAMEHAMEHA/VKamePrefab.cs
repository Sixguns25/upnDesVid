using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VKamePrefab : MonoBehaviour
{
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 4);
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
    }

    public void SetDirection(string direction)
    {
        if (direction == "left")
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);

        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemigo")
        {

            Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
    }
}
