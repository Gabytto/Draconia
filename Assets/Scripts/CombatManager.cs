using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatManager : MonoBehaviour
{
    [Header("Referencias de UI")]
    public Slider sliderP1;
    public Slider sliderP2;

    [Header("Referencias de Spawning")]
    public Transform spawnJugador;
    public Transform spawnEnemigo;

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
        if (ManejadorDeDatos.Instancia != null)
        {
            GameObject goP1 = Instantiate(ManejadorDeDatos.Instancia.dragonP1.gameObject, spawnJugador.position, Quaternion.identity);
            GameObject goP2 = Instantiate(ManejadorDeDatos.Instancia.dragonP2.gameObject, spawnEnemigo.position, Quaternion.identity);

            dragonJugador = goP1.GetComponent<DragonStats>();
            dragonEnemigo = goP2.GetComponent<DragonStats>();

            goP2.transform.localScale = new Vector3(-goP2.transform.localScale.x, goP2.transform.localScale.y, goP2.transform.localScale.z);

            // --- ARREGLO DE BARRAS DE VIDA ---
            // Seteamos el máximo y el valor inicial usando vidaMaxima (dato seguro)
            sliderP1.maxValue = dragonJugador.vidaMaxima;
            sliderP1.value = dragonJugador.vidaMaxima;

            sliderP2.maxValue = dragonEnemigo.vidaMaxima;
            sliderP2.value = dragonEnemigo.vidaMaxima;
        }

        StartCoroutine(BucleDeCombate());
    }

    IEnumerator BucleDeCombate()
    {
        yield return new WaitForSeconds(1f);
        if (dragonEnemigo.velocidad > dragonJugador.velocidad) esTurnoJugador = false;

        while (dragonJugador.vidaActual > 0 && dragonEnemigo.vidaActual > 0)
        {
            if (esTurnoJugador) Atacar(dragonJugador, dragonEnemigo);
            else Atacar(dragonEnemigo, dragonJugador);

            // Actualizamos visualmente las barras después de cada golpe
            sliderP1.value = dragonJugador.vidaActual;
            sliderP2.value = dragonEnemigo.vidaActual;

            esTurnoJugador = !esTurnoJugador;
            yield return new WaitForSeconds(pausaEntreTurnos);
        }

        Debug.Log("¡Combate terminado!");
    }

    void Atacar(DragonStats atacante, DragonStats objetivo)
    {
        float multiplicador = CalcularEfectividad(atacante.elemento, objetivo.elemento);
        float danioFinal = atacante.ataque * multiplicador;
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