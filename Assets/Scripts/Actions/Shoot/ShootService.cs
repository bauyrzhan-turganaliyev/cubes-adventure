using System;
using System.Collections.Generic;
using Actions.CubeActions;
using Configs;
using Infrastructure;
using UnityEngine;

namespace Actions.Shoot
{
    public class ShootService : MonoBehaviour
    {
        [SerializeField] private AnnounceView _announceView;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _bulletParent;
    
        [SerializeField] private Transform _bulletStart;

        public Action OnTryGetNewTarget;
        public Action<int> OnDestroyCubeByHit;
        public Action OnStopAction;
 
        private MessageBus _messageBus;
        private GameConfig _gameConfig;
        private BulletPool _bulletPool;

        private int _targetCubeID;
        private bool _isAutoShoot;
        private bool _isCanShoot;

        public void Init(MessageBus messageBus, GameConfig gameConfig)
        {
            _messageBus = messageBus;
            _gameConfig = gameConfig;
        
            _messageBus.OnCubesInitialized += RegisterCubes;
            _messageBus.OnSettingsChanged += SetupConfig;

            _bulletPool = new BulletPool(_bulletPrefab, _bulletParent, _bulletStart);
            _bulletPool.OnBulletDestroyedByTime += TryPrepareToShoot;

            SetupConfig();
        }
    
        public void PrepareToShoot()
        {
            _isCanShoot = true;
            OnTryGetNewTarget?.Invoke();
        }
    
        public void TryToShoot(Cube targetCube)
        {
            if (!_isCanShoot) return;
            
            _targetCubeID = targetCube.GetID();
        
            GiveVisualFeedback(targetCube);
            ShootBullet(targetCube.GetPosition());
        }
    
        private void SetupConfig()
        {
            Bullet.Speed = _gameConfig.BulletSpeed;
            Bullet.Lifetime = _gameConfig.BulletLifetime;
        
            _isAutoShoot = _gameConfig.AutoShoot;
        }

        private void RegisterCubes(Dictionary<int, Cube> cubes)
        {
            foreach (var pair in cubes)
            {
                pair.Value.OnHitBullet += CheckHitCube;
            }
        }
    
        private void GiveVisualFeedback(Cube targetCube)
        {
            _announceView.SetTargetNumber(targetCube.GetID());
        }

        private void CheckHitCube(int hitCubeID)
        {
            if (hitCubeID != _targetCubeID && _isCanShoot)
                PrepareToShoot();
            else 
            {
                if (_isCanShoot) OnDestroyCubeByHit?.Invoke(hitCubeID);
                if (_isAutoShoot && _isCanShoot) PrepareToShoot();
                else OnStopAction?.Invoke();
            }
        }

        private void ShootBullet(Vector3 targetPoint)
        {
            var newBullet = _bulletPool.GetBullet();
            newBullet.transform.position = _bulletStart.position;
            newBullet.SetLookAtTarget(targetPoint);
        }

        private void TryPrepareToShoot()
        {
            if (_isCanShoot) PrepareToShoot();
        }

        public void StopAction()
        {
            _isCanShoot = false;
        }
    }
}