using UnityEngine;

public class Zombie : MonoBehaviour
{

    [Header("Zombie Settings")]
    public float health = 10f;
    public float speed = 0.5f;
    public float daño = 2f;
    public float RangoAtaque = 0.6f;

    [Header("State")]
    public bool isAlive = true;
    public ZombieState currentState = ZombieState.caminando;

    private float h;
    private Plant objetivoPlant;

    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float roll = Random.value;
        if (roll < 0.2f)
        {
            health *= 2f;
            daño *= 1.5f;
            transform.localScale *= 1.3f;
            if (sr != null) sr.color = Color.magenta;
        }
        else if (roll < 0.4f)
        {
            speed *= 2f;
            if (sr != null) sr.color = Color.yellow;
        }

        //La ia me ayudo a hacer que los zombies sea diferentes de estaditicas
    }

    public void Simulate(float h)
    {
        if (!isAlive) return;
        this.h = h;

        switch (currentState)
        {
            case ZombieState.caminando:
                avanzando();
                break;
            case ZombieState.atacando:
                Attack();
                break;
        }

        Estado();
    }

    void avanzando()
    {
        Plant plant = EncontroUnaPlanta();
        if (plant != null)
        {
            objetivoPlant = plant;
            currentState = ZombieState.atacando;
            return;
        }

        transform.position += Vector3.left * speed * h;
    }

    void Attack()
    {
        if (objetivoPlant == null || !objetivoPlant.isAlive)
        {
            objetivoPlant = null;
            currentState = ZombieState.caminando;
            return;
        }

        objetivoPlant.TakeDamage(daño * h);
    }

    Plant EncontroUnaPlanta()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            RangoAtaque,
            LayerMask.GetMask("Plants")
        );

        if (hit.collider != null)
            return hit.collider.GetComponent<Plant>();

        return null;
    }

    public void RecibioDAño(float daño)
    {
        health -= daño;
    }

    void Estado()
    {
        if (health <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.left * RangoAtaque);
    }
}

