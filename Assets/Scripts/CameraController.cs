using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player; // El transform del personaje
    public Vector3 offset;   // Offset de la c�mara
    // Start is called before the first frame update
    private void LateUpdate()
    {
        // Solo actualiza la posici�n en el eje X
        transform.position = new Vector3(player.position.x - offset.x, player.position.y + offset.y, offset.z);
    }
}
