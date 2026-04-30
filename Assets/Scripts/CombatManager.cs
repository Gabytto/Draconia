using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class CombatManager : MonoBehaviour
{
    [Header("Referencias de UI Combate")]
    public Slider sliderP1;
    public Slider sliderP2;

    [Header("Referencias de UI Victoria")]
    public GameObject panelVictoria;
    public TextMeshProUGUI textoGanador;

    [Header("Referencias de Spawning")]
    public Transform spawnJugador;
    public Transform spawnEnemigo;

    private DragonStats dragonJugador;
    private DragonStats dragonEnemigo;

    [Header("Configuración de Turnos")]
    public bool esTurnoJugador = true;
    public float pausaEntreTurnos = 1.5f;

    void Start()
    {
        panelVictoria.SetActive(false);
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

            // Espejar al P2
            goP2.transform.localScale = new Vector3(-goP2.transform.localScale.x, goP2.transform.localScale.y, goP2.transform.localScale.z);

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

            // Esperamos un momento para que el proyectil viaje antes de actualizar los sliders
            yield return new WaitForSeconds(0.5f);

            sliderP1.value = dragonJugador.vidaActual;
            sliderP2.value = dragonEnemigo.vidaActual;

            esTurnoJugador = !esTurnoJugador;
            yield return new WaitForSeconds(pausaEntreTurnos);
        }

        // --- LÓGICA DE VICTORIA ---
        string jugadorGanador = (dragonJugador.vidaActual > 0) ? "PLAYER 1" : "PLAYER 2";

        panelVictoria.SetActive(true);
        textoGanador.text = "¡" + jugadorGanador + " WIN!";
    }

    void Atacar(DragonStats atacante, DragonStats objetivo)
    {
        float multiplicador = CalcularEfectividad(atacante.elemento, objetivo.elemento);
        float danioFinal = atacante.ataque * multiplicador;

        // Disparo de proyectil
        if (atacante.prefabAtaque != null)
        {
            GameObject goProyectil = Instantiate(atacante.prefabAtaque, atacante.transform.position, Quaternion.identity);
            Proyectil scriptProyectil = goProyectil.GetComponent<Proyectil>();
            scriptProyectil.Configurar(objetivo.transform, danioFinal);
        }
        else
        {
            // Daño directo por si no hay prefab asignado
            objetivo.RecibirDanio(danioFinal);
        }
    }

    public void VolverALaSeleccion()
    {
        SceneManager.LoadScene("MenuPrincipal");
    }

    float CalcularEfectividad(DragonStats.TipoElemento a, DragonStats.TipoElemento o)
    {
        if (a == DragonStats.TipoElemento.Agua && o == DragonStats.TipoElemento.Fuego) return 2.0f;
        if (a == DragonStats.TipoElemento.Fuego && o == DragonStats.TipoElemento.Planta) return 2.0f;
        if (a == DragonStats.TipoElemento.Planta && o == DragonStats.TipoElemento.Agua) return 2.0f;
        return 1.0f;
    }
}