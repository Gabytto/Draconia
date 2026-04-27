using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectorDragones : MonoBehaviour
{
    [Header("Visualizadores Grandes")]
    public Image imagenP1;
    public TextMeshProUGUI nombreP1;
    public Image imagenP2;
    public TextMeshProUGUI nombreP2;

    [Header("Configuración de Grilla")]
    public DragonStats[] listaDragones;
    public GameObject[] marcosP1;
    public GameObject[] marcosP2;

    private int indexP1 = 0;
    private int indexP2 = 4;

    // Nuevas variables para confirmar
    private bool listoP1 = false;
    private bool listoP2 = false;

    void Start()
    {
        if (listaDragones.Length > 0) ActualizarInterfaz();
    }

    void Update()
    {
        // --- CONTROLES P1 ---
        if (!listoP1) // Solo se mueve si NO está listo
        {
            if (Input.GetButtonDown("HorizontalP1"))
            {
                int h = (int)Input.GetAxisRaw("HorizontalP1");
                indexP1 = Mathf.Clamp(indexP1 + h, 0, listaDragones.Length - 1);
                ActualizarInterfaz();
            }
        }

        if (Input.GetButtonDown("ConfirmP1"))
        {
            listoP1 = !listoP1; // Cambia entre listo y no listo
            ActualizarVisualConfirmacion();
        }

        // --- CONTROLES P2 ---
        if (!listoP2)
        {
            if (Input.GetButtonDown("HorizontalP2"))
            {
                int h = (int)Input.GetAxisRaw("HorizontalP2");
                indexP2 = Mathf.Clamp(indexP2 + h, 0, listaDragones.Length - 1);
                ActualizarInterfaz();
            }
        }

        if (Input.GetButtonDown("ConfirmP2"))
        {
            listoP2 = !listoP2;
            ActualizarVisualConfirmacion();
        }
    }

    void ActualizarInterfaz()
    {
        if (listaDragones.Length == 0) return;

        imagenP1.sprite = listaDragones[indexP1].fotoDragon;
        nombreP1.text = listaDragones[indexP1].nombreDragon;
        imagenP2.sprite = listaDragones[indexP2].fotoDragon;
        nombreP2.text = listaDragones[indexP2].nombreDragon;

        for (int i = 0; i < listaDragones.Length; i++)
        {
            if (marcosP1.Length > i) marcosP1[i].SetActive(i == indexP1);
            if (marcosP2.Length > i) marcosP2[i].SetActive(i == indexP2);
        }
    }

    void ActualizarVisualConfirmacion()
    {
        // --- Jugador 1 ---
        if (listoP1)
        {
            nombreP1.text = "READY!";
            nombreP1.color = Color.yellow;
        }
        else
        {
            // Si cancela, volvemos a poner el nombre del dragón actual
            nombreP1.text = listaDragones[indexP1].nombreDragon;
            nombreP1.color = Color.white;
        }

        // --- Jugador 2 ---
        if (listoP2)
        {
            nombreP2.text = "READY!";
            nombreP2.color = Color.yellow;
        }
        else
        {
            nombreP2.text = listaDragones[indexP2].nombreDragon;
            nombreP2.color = Color.white;
        }

        if (listoP1 && listoP2)
        {
            Debug.Log("¡AMBOS LISTOS! Cargando pelea...");
            // Aquí llamaremos al futuro script de cambio de escena
        }
    }
}