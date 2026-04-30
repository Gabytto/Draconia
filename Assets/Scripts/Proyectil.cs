using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidad = 12f;

    private float danio;
    private Transform objetivo;
    private bool impactado = false;

    // Esta función la llama el CombatManager al disparar
    public void Configurar(Transform target, float danioAtaque)
    {
        objetivo = target;
        danio = danioAtaque;

        // Rotar el proyectil para que mire hacia donde va
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    void Update()
    {
        if (objetivo == null || impactado) return;

        // Movimiento suave hacia el enemigo
        transform.position = Vector2.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);

        // Detectar si llegó al objetivo
        if (Vector2.Distance(transform.position, objetivo.position) < 0.2f)
        {
            Impactar();
        }
    }

    void Impactar()
    {
        impactado = true;

        DragonStats statsEnemigo = objetivo.GetComponent<DragonStats>();
        if (statsEnemigo != null)
        {
            statsEnemigo.RecibirDanio(danio);
        }

        // Podés agregar aquí un Instantiate de una explosión si tenés el prefab
        Destroy(gameObject);
    }
}