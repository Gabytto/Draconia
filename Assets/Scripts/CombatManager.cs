using UnityEngine;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    public DragonStats dragonJugador;
    public DragonStats dragonEnemigo;

    [Header("Configuración de Turnos")]
    public bool esTurnoJugador = true;
    public float pausaEntreTurnos = 1.0f;

    void Start()
    {
        // Pequeño delay para que cargue todo bien
        StartCoroutine(BucleDeCombate());
    }

    IEnumerator BucleDeCombate()
    {
        // Esperamos un segundo antes de empezar la masacre
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

            // ESTA LINEA ES CLAVE: Detiene el código por el tiempo que pusiste en el inspector
            yield return new WaitForSeconds(pausaEntreTurnos);
        }

        string ganador = dragonJugador.vidaActual > 0 ? dragonJugador.nombreDragon : dragonEnemigo.nombreDragon;
        Debug.Log("¡Combate terminado! El ganador es: " + ganador);
    }

    void Atacar(DragonStats atacante, DragonStats objetivo)
    {
        Debug.Log(atacante.nombreDragon + " lanza un ataque contra " + objetivo.nombreDragon);
        objetivo.RecibirDanio(atacante.ataque);
    }
}