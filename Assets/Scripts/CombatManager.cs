using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement; // Necesario para volver a la selección
using TMPro;

public class CombatManager : MonoBehaviour
{
    [Header("Referencias de UI Combate")]
    public Slider sliderP1;
    public Slider sliderP2;

    [Header("Referencias de UI Victoria")]
    public GameObject panelVictoria; // Arrastrá el PanelVictoria aquí
    public TextMeshProUGUI textoGanador;

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
        // Al empezar nos aseguramos de que el panel esté apagado
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

            sliderP1.value = dragonJugador.vidaActual;
            sliderP2.value = dragonEnemigo.vidaActual;

            esTurnoJugador = !esTurnoJugador;
            yield return new WaitForSeconds(pausaEntreTurnos);
        }

        // --- LÓGICA DE VICTORIA ---
        string nombreGanador = (dragonJugador.vidaActual > 0) ? dragonJugador.nombreDragon : dragonEnemigo.nombreDragon;

        Debug.Log("¡Combate terminado! Ganador: " + nombreGanador);

        // Activamos el panel y ponemos el nombre
        panelVictoria.SetActive(true);
        textoGanador.text = "¡" + nombreGanador.ToUpper() + " ES EL GANADOR!";
    }

    void Atacar(DragonStats atacante, DragonStats objetivo)
    {
        float multiplicador = CalcularEfectividad(atacante.elemento, objetivo.elemento);
        float danioFinal = atacante.ataque * multiplicador;
        objetivo.RecibirDanio(danioFinal);
    }

    // Función para el botón de la UI
    public void VolverALaSeleccion()
    {
        SceneManager.LoadScene("Seleccion"); // Ajustá el nombre si es "SeleccionarDragones"
    }

    float CalcularEfectividad(DragonStats.TipoElemento a, DragonStats.TipoElemento o)
    {
        if (a == DragonStats.TipoElemento.Agua && o == DragonStats.TipoElemento.Fuego) return 2.0f;
        if (a == DragonStats.TipoElemento.Fuego && o == DragonStats.TipoElemento.Planta) return 2.0f;
        if (a == DragonStats.TipoElemento.Planta && o == DragonStats.TipoElemento.Agua) return 2.0f;
        return 1.0f;
    }
}