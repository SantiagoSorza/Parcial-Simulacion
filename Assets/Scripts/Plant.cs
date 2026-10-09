using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("Plant Settings")]
    public float health = 15f;
    public float shootInterval = 1.5f;
    public float visionRange = 10f;
    public GameObject projectilePrefab;

    [Header("State")]
    public bool isAlive = true;
    public PlantState currentState = PlantState.intativo;

    private float h;
    private float shootTimer = 0f;

    public void Simulate(float h)
    {
        if (!isAlive) return;
        this.h = h;

        switch (currentState)
        {
            case PlantState.intativo:
                Idle();
                break;
            case PlantState.activo:
                Shoot();
                break;
        }

        CheckState();
    }

    void Idle()
    {
        if (ZombieInRow())
            currentState = PlantState.activo;
    }

    void Shoot()
    {
        if (!ZombieInRow())
        {
            currentState = PlantState.intativo;
            shootTimer = 0f;
            return;
        }

        shootTimer += h;
        if (shootTimer >= shootInterval)
        {
            shootTimer = 0f;
            Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        }
    }

    bool ZombieInRow()
    {
        // Raycast hacia la derecha: ¿hay un zombi en mi fila?
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position + Vector3.right * 0.5f,
            Vector2.right,
            visionRange,
            LayerMask.GetMask("Zombies")
        );
        return hit.collider != null;
    }

    public void TakeDamage(float dmg)
    {
        health -= dmg;
    }

    void CheckState()
    {
        if (health <= 0)
        {
            isAlive = false;
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * visionRange);
    }
}

