using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManejadorDeDatos : MonoBehaviour
{
    public static ManejadorDeDatos Instancia;

    [Header("Datos de la Partida")]
    public DragonStats dragonP1;
    public DragonStats dragonP2;

    void Awake()
    {
        // Patrón Singleton: si ya existe uno, se destruye el nuevo
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject); // ¡La línea mágica!
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
