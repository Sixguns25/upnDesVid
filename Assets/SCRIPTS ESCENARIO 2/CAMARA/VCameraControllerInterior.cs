using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VCameraControllerInterior : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    void Start()
    {
        //player = GameObject.Find("Goku");
    }

    // Update is called once per frame
    void Update()
    {
        float x=player.transform.position.x+10;
        float y = player.transform.position.y;
        transform.position=new Vector3(x,y,transform.position.z);
    }
}
