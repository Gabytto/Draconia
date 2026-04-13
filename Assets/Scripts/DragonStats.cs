using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonStats : MonoBehaviour
{
    [Header("Identidad")]
    public string nombreDragon;
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
        // Al empezar el juego, el dragón nace con la vida a tope
        vidaActual = vidaMaxima;
    }

    // Método simple para recibir daño
    public void RecibirDanio(float cantidad)
    {
        // El daño final resta la defensa para que sea más justo
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
        // Por ahora solo lo desactivamos, después podemos poner una animación
        gameObject.SetActive(false);
    }
}
