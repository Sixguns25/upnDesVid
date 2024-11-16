using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ControladorDatosJuego : MonoBehaviour
{
    public GameObject jugador;
    public string archivoDeGuardado;
    public DatosJuego datosJuego = new DatosJuego();

    private void Awake()
    {
        archivoDeGuardado = Application.dataPath + "/datosJuego.json";
        jugador = GameObject.FindGameObjectWithTag("Hero");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            CargarDatos();
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            GuardarDatos();
        }
    }

    private void CargarDatos()
    {
        if (File.Exists(archivoDeGuardado))
        {
            string contenido = File.ReadAllText(archivoDeGuardado);
            datosJuego = JsonUtility.FromJson<DatosJuego>(contenido);

            Debug.Log("Posicion del jugador: " + datosJuego.posicion);

            jugador.transform.position = datosJuego.posicion;
            jugador.GetComponent<VidaJugador>().vidaActual = datosJuego.vida;
        }
        else
        {
            Debug.Log("El archivo no existe");
        }
    }

    private void GuardarDatos()
    {
        DatosJuego nuevosDatos = new DatosJuego()
        {
            posicion = jugador.transform.position,
            vida = jugador.GetComponent<VidaJugador>().vidaActual
        };

        string cadenJSON = JsonUtility.ToJson(nuevosDatos);

        File.WriteAllText(archivoDeGuardado, cadenJSON);

        Debug.Log("Archivo guardado");
    }
}
