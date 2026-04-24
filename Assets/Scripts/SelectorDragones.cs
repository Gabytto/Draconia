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
    // Arrastrá aquí los 5 Buttons de la Jerarquía
    public DragonStats[] listaDragones;

    [Header("Marcos de Selección")]
    public GameObject[] marcosP1; // Los 5 Selector_P1 verdes
    public GameObject[] marcosP2; // Los 5 Selector_P2 amarillos

    private int indexP1 = 0;
    private int indexP2 = 4;

    void Start()
    {
        // Pequeña validación para no tener errores al dar Play
        if (listaDragones.Length > 0)
        {
            ActualizarInterfaz();
        }
    }

    void Update()
    {
        // Movimiento P1
        if (Input.GetButtonDown("HorizontalP1"))
        {
            int h = (int)Input.GetAxisRaw("HorizontalP1");
            indexP1 = Mathf.Clamp(indexP1 + h, 0, listaDragones.Length - 1);
            ActualizarInterfaz();
        }

        // Movimiento P2
        if (Input.GetButtonDown("HorizontalP2"))
        {
            int h = (int)Input.GetAxisRaw("HorizontalP2");
            indexP2 = Mathf.Clamp(indexP2 + h, 0, listaDragones.Length - 1);
            ActualizarInterfaz();
        }
    }

    void ActualizarInterfaz()
    {
        // Seguridad: si no hay dragones en la lista, salimos
        if (listaDragones.Length == 0) return;

        // Actualización P1
        if (listaDragones[indexP1] != null)
        {
            imagenP1.sprite = listaDragones[indexP1].fotoDragon;
            nombreP1.text = listaDragones[indexP1].nombreDragon;
        }

        // Actualización P2
        if (listaDragones[indexP2] != null)
        {
            imagenP2.sprite = listaDragones[indexP2].fotoDragon;
            nombreP2.text = listaDragones[indexP2].nombreDragon;
        }

        // Control de los marcos en la grilla
        for (int i = 0; i < listaDragones.Length; i++)
        {
            if (marcosP1.Length > i) marcosP1[i].SetActive(i == indexP1);
            if (marcosP2.Length > i) marcosP2[i].SetActive(i == indexP2);
        }
    }
}