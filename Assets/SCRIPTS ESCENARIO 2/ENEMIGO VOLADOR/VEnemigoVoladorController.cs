using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VEnemigoVoladorController : MonoBehaviour
{
    public GameObject FuegoEnemigoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LanzarBolaFuego()
    {
        GameObject fuego = Instantiate(FuegoEnemigoPrefab, transform.position, Quaternion.Euler(0,0,-90));

    }

}
