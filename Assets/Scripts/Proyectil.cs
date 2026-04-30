using UnityEngine;

public class Proyectil : MonoBehaviour
{
    [Header("Configuración")]
    public float velocidad = 12f;

    private float danio;
    private Transform objetivo;
    private bool impactado = false;

    public void Configurar(Transform target, float danioAtaque)
    {
        objetivo = target;
        danio = danioAtaque;

        // Rotar el proyectil para que mire hacia el objetivo
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        float angulo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angulo);
    }

    void Update()
    {
        if (objetivo == null || impactado) return;

        transform.position = Vector2.MoveTowards(transform.position, objetivo.position, velocidad * Time.deltaTime);

        if (Vector2.Distance(transform.position, objetivo.position) < 0.2f)
        {
            Impactar();
        }
    }

    void Impactar()
    {
        if (impactado) return;
        impactado = true;

        DragonStats statsEnemigo = objetivo.GetComponent<DragonStats>();
        if (statsEnemigo != null)
        {
            statsEnemigo.RecibirDanio(danio);

            // Buscamos el Manager para actualizar la barra de vida al instante
            CombatManager manager = FindObjectOfType<CombatManager>();
            if (manager != null)
            {
                manager.ActualizarBarrasVida();
            }
        }

        Destroy(gameObject);
    }
}