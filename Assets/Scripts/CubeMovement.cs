using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Скорость перемещения кубов

    private Rigidbody rb;
    private Vector3 velocity;
    private bool isInit;

    private void Start()
    {
        Invoke("Begin", 2);
    }

    private void Begin()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        velocity = randomDirection * moveSpeed;
        velocity.y = 0;
        rb.AddForce(velocity, ForceMode.Force);
        isInit = true;
    }
    private void FixedUpdate()
    {
        if (!isInit) return;
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }
    void OnCollisionEnter(Collision collision)
    {
        ReflectProjectile(collision.contacts[0].normal);
    }

    private void ReflectProjectile(Vector3 reflectVector)
    {
        if (!isInit) return;
        velocity = Vector3.Reflect(velocity, reflectVector);
        rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
    }
}