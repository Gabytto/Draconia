using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonStats : MonoBehaviour
{
    [Header("Identidad")]
    public string nombreDragon;
    public Sprite fotoDragon;
    public enum TipoElemento { Electrico, Fuego, Agua, Planta, Roca }
    public TipoElemento elemento;

    [Header("Visuales de Ataque")]
    public GameObject prefabAtaque; // <-- El proyectil específico de este dragón

    [Header("Estadísticas de Combate")]
    public float vidaMaxima = 100f;
    public float vidaActual;
    public float ataque = 20f;
    public float defensa = 10f;
    public float velocidad = 5f;

    void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void RecibirDanio(float cantidad)
    {
        // Ajuste para que la defensa no anule todo el ataque
        float danioFinal = Mathf.Max(cantidad - (defensa * 0.5f), 5f);
        vidaActual -= danioFinal;

        Debug.Log(nombreDragon + " recibió " + danioFinal + " de daño. Vida restante: " + vidaActual);

        if (vidaActual <= 0)
        {
            vidaActual = 0;
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log(nombreDragon + " ha sido derrotado.");
        gameObject.SetActive(false);
    }
}