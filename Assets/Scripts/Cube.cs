using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private TMP_Text _idText;

    public Action<int> OnDestroy;

    private int _id;
    private bool _isReady;
    private Vector3 _velocity;

    public void Move(float moveSpeed)
    {
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        
        _velocity = randomDirection * moveSpeed;            
        _rigidbody.AddForce(_velocity, ForceMode.Force);
        _isReady = true;
    }
    
    private void FixedUpdate()
    {
        if (_isReady)
            _rigidbody.velocity = new Vector3(_velocity.x, _rigidbody.velocity.y, _velocity.z);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            OnDestroy?.Invoke(_id);
            Destroy(gameObject);
            return;
        }
        
        ReflectProjectile(collision.contacts[0].normal);
    }

    public void SetNumber(int id)
    {
        _id = id;
        _idText.text = _id.ToString();
    }

    private void ReflectProjectile(Vector3 reflectVector)
    {
        if (!_isReady) return;
        
        _velocity = Vector3.Reflect(_velocity, reflectVector);
        _rigidbody.velocity = new Vector3(_velocity.x, _rigidbody.velocity.y, _velocity.z);
    }

    public int GetID()
    {
        return _id;
    }
}