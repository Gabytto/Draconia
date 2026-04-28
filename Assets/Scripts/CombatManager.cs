using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    [Header("Referencias de Spawning")]
    public Transform spawnJugador; // Arrastrá un Empty Object aquí
    public Transform spawnEnemigo; // Arrastrá otro Empty Object aquí

    // Estas se llenarán solas al empezar
    private DragonStats dragonJugador;
    private DragonStats dragonEnemigo;

    [Header("Configuración de Turnos")]
    public bool esTurnoJugador = true;
    public float pausaEntreTurnos = 1.0f;

    void Start()
    {
        PrepararCombate();
    }

    void PrepararCombate()
    {
        // 1. Buscamos los datos guardados en el Manejador persistente
        if (ManejadorDeDatos.Instancia != null)
        {
            // 2. Instanciamos los prefabs de los dragones en los puntos de spawn
            // Usamos .gameObject para instanciar el objeto completo
            GameObject goP1 = Instantiate(ManejadorDeDatos.Instancia.dragonP1.gameObject, spawnJugador.position, Quaternion.identity);
            GameObject goP2 = Instantiate(ManejadorDeDatos.Instancia.dragonP2.gameObject, spawnEnemigo.position, Quaternion.identity);

            // 3. Obtenemos el componente DragonStats de los clones recién creados
            dragonJugador = goP1.GetComponent<DragonStats>();
            dragonEnemigo = goP2.GetComponent<DragonStats>();

            // Opcional: Invertir la escala del P2 para que mire hacia el P1
            goP2.transform.localScale = new Vector3(-goP2.transform.localScale.x, goP2.transform.localScale.y, goP2.transform.localScale.z);
        }
        else
        {
            Debug.LogError("¡No hay ManejadorDeDatos! Asegurate de venir desde la escena de Selección.");
            return;
        }

        // 4. Arrancamos el bucle
        StartCoroutine(BucleDeCombate());
    }

    IEnumerator BucleDeCombate()
    {
        yield return new WaitForSeconds(1f);

        if (dragonEnemigo.velocidad > dragonJugador.velocidad)
        {
            esTurnoJugador = false;
        }

        Debug.Log("¡Comienza el combate!");

        while (dragonJugador.vidaActual > 0 && dragonEnemigo.vidaActual > 0)
        {
            if (esTurnoJugador)
            {
                Atacar(dragonJugador, dragonEnemigo);
            }
            else
            {
                Atacar(dragonEnemigo, dragonJugador);
            }

            esTurnoJugador = !esTurnoJugador;
            yield return new WaitForSeconds(pausaEntreTurnos);
        }

        string ganador = dragonJugador.vidaActual > 0 ? dragonJugador.nombreDragon : dragonEnemigo.nombreDragon;
        Debug.Log("¡Combate terminado! El ganador es: " + ganador);
    }

    void Atacar(DragonStats atacante, DragonStats objetivo)
    {
        float multiplicador = CalcularEfectividad(atacante.elemento, objetivo.elemento);
        float danioFinal = atacante.ataque * multiplicador;

        Debug.Log(atacante.nombreDragon + " ataca a " + objetivo.nombreDragon + ". Efectividad: x" + multiplicador);
        objetivo.RecibirDanio(danioFinal);
    }

    float CalcularEfectividad(DragonStats.TipoElemento a, DragonStats.TipoElemento o)
    {
        if (a == DragonStats.TipoElemento.Agua && o == DragonStats.TipoElemento.Fuego) return 2.0f;
        if (a == DragonStats.TipoElemento.Fuego && o == DragonStats.TipoElemento.Planta) return 2.0f;
        if (a == DragonStats.TipoElemento.Planta && o == DragonStats.TipoElemento.Agua) return 2.0f;
        return 1.0f;
    }
}