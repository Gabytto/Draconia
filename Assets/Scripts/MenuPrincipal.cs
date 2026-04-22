using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    public void JugarSolo()
    {
        Debug.Log("Modo 1 jugador seleccionado");
        SceneManager.LoadScene("SeleccionDragones");
    }
    public void JugarMultijugador()
    {
        Debug.Log("Modo 2 jugadores seleccionado");
        SceneManager.LoadScene("SeleccionDragones");
    }
    public void SalirDelJuego()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}
