using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SelectorDragones : MonoBehaviour
{
    [Header("Referencias de UI")]
    public Image imagenGrandeP1;
    public TextMeshProUGUI textoNombreP1;

    public Image imagenGrandeP2;
    public TextMeshProUGUI textoNombreP2;

    [Header("Base de Datos de Dragones")]
    // Aquí arrastra los ScriptableObjects o los Prefabs de tus dragones
    public DragonStats[] todosLosDragones;

    // Esta función la vinculas al OnClick de cada botón de la grilla
    public void PrevisualizarDragon(int index)
    {
        DragonStats seleccionado = todosLosDragones[index];

        // Por ahora, supongamos que el P1 es quien está eligiendo
        // Luego podemos agregar lógica para que el P2 elija después
        ActualizarVisualizacion(imagenGrandeP1, textoNombreP1, seleccionado);
    }

    void ActualizarVisualizacion(Image img, TextMeshProUGUI txt, DragonStats stats)
    {
        // Importante: El Sprite que usas aquí debe venir de tus assets de arte
        // Si usas el script que tenías, asegúrate de tener una variable 'public Sprite fotoMenu' en DragonStats
        img.sprite = stats.fotoDragon;
        txt.text = stats.nombreDragon;

        // Activamos la imagen por si estaba transparente/oculta
        img.gameObject.SetActive(true);
    }
}