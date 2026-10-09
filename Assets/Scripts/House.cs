using UnityEngine;

public class House : MonoBehaviour
{
    public float vida = 100f;
    public bool pierde = false;

    public void TakeDamage(float daño)
    {
        vida -= daño;
        if (vida <= 0) pierde = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Zombie>() != null)
        {
            pierde = true;
        }
    }
}

