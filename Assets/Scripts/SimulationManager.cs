using Unity.VisualScripting;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public float secondsPerIteration = 0.1f;
    private float time = 0f;

    public House house;
    public SpawnerZombie zombieSpawner;

    public bool simulationOver = false;

    void Start()
    {
        house = FindFirstObjectByType<House>();
        zombieSpawner = FindFirstObjectByType<SpawnerZombie>();
    }

    void Update()
    {
        if (simulationOver) return;

        time += Time.deltaTime;

        if (time >= secondsPerIteration)
        {
            time = 0f;
            Simulate();
        }
    }

    void Simulate()
    {
        Plant[] plants = FindObjectsByType<Plant>(FindObjectsSortMode.None);
        Zombie[] zombies = FindObjectsByType<Zombie>(FindObjectsSortMode.None);

        foreach (Plant p in plants)
            if (p != null && p.isAlive) p.Simulate(secondsPerIteration);

        foreach (Zombie z in zombies)
            if (z != null && z.isAlive) z.Simulate(secondsPerIteration);

        if (zombieSpawner != null)
            zombieSpawner.Simulate(secondsPerIteration);

        // Condición de derrota
        if (house != null && house.pierde)
        {
            simulationOver = true;
            Debug.Log("¡Los zombis entraron! Simulación terminada.");
        }
    }
}