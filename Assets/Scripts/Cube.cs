using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private TMP_Text _idText;

    public Action<int> OnDestroyed;
    public Action<int> OnAte;
    
    public bool IsGrow;

    private int _id;
    private float _moveSpeed;
    private bool _isReady;
    private Vector3 _velocity;
    private Cube _target;

    public void Move(float moveSpeed)
    {
        _moveSpeed = moveSpeed;
        Vector3 randomDirection = Random.insideUnitSphere.normalized;
        
        _velocity = randomDirection * _moveSpeed;            
        _rigidbody.AddForce(_velocity, ForceMode.Force);
        _isReady = true;
    }

    public void SetTarget(Cube target)
    {
        _target = target;
    }
    
    private void FixedUpdate()
    {
        if (_isReady)
            if (_target == null)
                _rigidbody.velocity = new Vector3(_velocity.x, _rigidbody.velocity.y, _velocity.z);
            else
            {
                // Получите направление к движущемуся кубу
                Vector3 direction = _target.transform.position - transform.position;

                // Используйте LookRotation, чтобы следящий куб поворачивался в направлении движущегося куба
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));

                // Примените плавное вращение к следящему кубу
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _moveSpeed);

                // Следуйте за движущимся кубом, двигаясь вперед
                transform.position += transform.forward * _moveSpeed * Time.deltaTime;
            }
    }

    public void OnCollisionEnter(Collision collision)
    {
        var collisionCopy = collision;
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy();
            return;
        }

        if (collision.gameObject.TryGetComponent<Cube>(out Cube cube))
        {
            if (IsGrow)
            {
                cube.Destroy();
                OnAte?.Invoke(_id);
            }
        }

        ReflectProjectile(collisionCopy.contacts[0].normal);
    }

    private void Destroy()
    {
        OnDestroyed?.Invoke(_id);
        Destroy(gameObject);
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

    public void Grow()
    {
        IsGrow = true;
        transform.DOScale(new Vector3(2, 2, 2), 1);
    }
}