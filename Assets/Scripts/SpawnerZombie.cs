using UnityEngine;

public class SpawnerZombie : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject zombiePrefab;
    public float spawnInterval = 3f;
    public int maxZombies = 20;

    [Header("Filas")]
    public int rows = 5;
    public float rowSpacing = 1.5f;

    private float time = 0f;

    public void Simulate(float h)
    {
        time += h;

        if (time >= spawnInterval)
        {
            time = 0f;

            if (CountZombies() < maxZombies)
                SpawnZombie();
        }
    }

    void SpawnZombie()
    {
        int row = Random.Range(0, rows);
        float y = (row - (rows - 1) / 2f) * rowSpacing;

        Vector3 pos = transform.position + new Vector3(0, y, 0);
        Instantiate(zombiePrefab, pos, Quaternion.identity);
    }

    int CountZombies()
    {
        return FindObjectsByType<Zombie>(FindObjectsSortMode.InstanceID).Length;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        for (int i = 0; i < rows; i++)
        {
            float y = (i - (rows - 1) / 2f) * rowSpacing;
            Gizmos.DrawWireSphere(transform.position + new Vector3(0, y, 0), 0.3f);
        }
    }
}
