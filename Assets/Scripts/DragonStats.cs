using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonStats : MonoBehaviour
{
    [Header("Identidad")]
    public string nombreDragon;
    public Sprite fotoDragon; // <--- ESTA ES LA LÍNEA QUE FALTABA
    public enum TipoElemento { Electrico, Fuego, Agua, Planta, Roca }
    public TipoElemento elemento;

    [Header("Estadísticas de Combate")]
    public float vidaMaxima = 100f;
    public float vidaActual;
    public float ataque = 20f;
    public float defensa = 10f;
    public float velocidad = 5f;

    void Start()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDanio(float cantidad)
    {
        float danioFinal = Mathf.Max(cantidad - defensa, 1);
        vidaActual -= danioFinal;

        Debug.Log(nombreDragon + " recibió " + danioFinal + " de daño. Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log(nombreDragon + " ha sido derrotado.");
        gameObject.SetActive(false);
    }
}