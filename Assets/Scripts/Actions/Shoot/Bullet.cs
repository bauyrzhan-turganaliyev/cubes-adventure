using System;
using UnityEngine;

namespace Actions.Shoot
{
    public class Bullet : MonoBehaviour
    {
        public static float Speed { get; set; }
        public static float Lifetime { get; set; }

        private Vector3 _targetPoint;
        private float _timer = 0f;

        public Action OnDestroyByTime;
    
        public void SetLookAtTarget(Vector3 targetPoint)
        {
            _targetPoint = targetPoint;
            transform.LookAt(_targetPoint);
        }

        private void OnEnable()
        {
            _timer = 0;
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * Speed * Time.deltaTime, Space.Self);

            if (!gameObject.activeInHierarchy) return;
            _timer += Time.deltaTime;
            if (_timer >= Lifetime)
            {
                OnDestroyByTime?.Invoke();
                gameObject.SetActive(false);
            }
        }
    
        private void OnTriggerEnter(Collider other)
        {
            gameObject.SetActive(false);
        }
    }
}