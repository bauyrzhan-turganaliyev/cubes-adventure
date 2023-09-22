using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Bullet speed
    public float lifetime = 2f; // Bullet lifetime (in seconds)

    private float timer = 0f;
    private Vector3 _targetPoint;

    // Set the target point when creating the bullet
    public void SetLookAtTarget(Vector3 targetPoint)
    {
        _targetPoint = targetPoint;
        transform.LookAt(_targetPoint);
    }

    private void Update()
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
        Destroy(gameObject);
    }
}