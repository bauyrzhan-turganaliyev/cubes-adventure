using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Actions.CubeActions
{
    public class Cube : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private TMP_Text _idText;

        public Action<int> OnHitBullet;
        public Action<int> OnDestroyed;
        public Action<Cube> OnAte;
    
        private bool _isGrow;

        private int _id;
        private float _moveSpeed;
        private bool _isCanMove;
        private Vector3 _velocity;
        private Cube _target;

        public void Init()
        {
            _renderer.material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);
        }

        public void Move(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
            Vector3 randomDirection = Random.insideUnitSphere.normalized;
        
            _velocity = randomDirection * _moveSpeed;            
            _rigidbody.AddForce(_velocity, ForceMode.Force);
            _isCanMove = true;
        }
        public void Stop()
        {
            _isCanMove = false;
        }
        public void SetTarget(Cube target)
        {
            _target = target;
            if (_target == null)
            {
                SwitchGrow(false);
            }
        }
    
        private void FixedUpdate()
        {
            if (_isCanMove)
                if (_target == null)
                    _rigidbody.velocity = new Vector3(_velocity.x, _rigidbody.velocity.y, _velocity.z);
                else
                {
                    Vector3 direction = _target.transform.position - transform.position;
                    Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _moveSpeed);
                    transform.position += transform.forward * _moveSpeed * Time.deltaTime;
                }
        }
        
        public void Destroy()
        {
            OnDestroyed?.Invoke(_id);
            Deactivate();
        }

        public void Deactivate()
        {
            _isCanMove = false;
            gameObject.SetActive(false);
        }

        public void SetNumber(int id)
        {
            _id = id;
            _idText.text = _id.ToString();
        }

        public int GetID()
        {
            return _id;
        }

        public void SwitchGrow(bool isGrow)
        {
            _isGrow = isGrow;
            var newGrowValue = isGrow ? new Vector3(2, 2, 2) : new Vector3(1, 1, 1);
            transform.DOScale(newGrowValue, 1);
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }
        private void OnTriggerEnter(Collider other)
        {        
            if (other.gameObject.CompareTag("Bullet"))
            {
                OnHitBullet?.Invoke(_id);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            var collisionCopy = collision;


            if (collision.gameObject.TryGetComponent<Cube>(out Cube cube))
            {
                if (_isGrow)
                {
                    OnAte?.Invoke(cube);
                }
            }

            ReflectProjectile(collisionCopy.contacts[0].normal);
        }
        
        private void ReflectProjectile(Vector3 reflectVector)
        {
            if (!_isCanMove) return;
        
            _velocity = Vector3.Reflect(_velocity, reflectVector);
            _rigidbody.velocity = new Vector3(_velocity.x, _rigidbody.velocity.y, _velocity.z);
        }

    }
}