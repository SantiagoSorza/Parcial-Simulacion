using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public float daño = 2f;
    public float duracion = 6f;

    private void Start()
    {
        Destroy(gameObject, duracion); 
    }

    private void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Zombie zombie = other.GetComponent<Zombie>();
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.left,
            LayerMask.GetMask("Zombie")
        );

        if (zombie != null)
        {
            zombie.RecibioDAño(daño);
            Destroy(gameObject);
        }
    }
}
