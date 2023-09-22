using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Bullet speed
    public float lifetime = 2f; // Bullet lifetime (in seconds)

    private float timer = 0f;

    // Set the target point when creating the bullet
    public void SetLookAtTarget(Vector3 targetPoint)
    {
        transform.LookAt(targetPoint);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
        
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Handle collision logic here if needed
        // For example, you can check if the bullet hits an enemy, and apply damage.

        // Destroy the bullet on collision
        Destroy(gameObject);
    }
}