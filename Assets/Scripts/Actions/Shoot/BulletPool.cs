using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Actions.Shoot
{
    public class BulletPool
    {
        public Action OnBulletDestroyedByTime;

        private ObjectPool<Bullet> _bulletPool;

        public BulletPool(Bullet prefab, Transform parent, Transform bulletStart, int initialSize = 5)
        {
            _bulletPool = new ObjectPool<Bullet>(
                prefab,
                parent,
                () =>
                {
                    Bullet bullet = Object.Instantiate(prefab, bulletStart.position, Quaternion.identity);
                    bullet.OnDestroyByTime += () => OnBulletDestroyedByTime?.Invoke();
                    bullet.transform.SetParent(parent);
                    return bullet;
                },
                initialSize
            );
        }

        public Bullet GetBullet()
        {
            return _bulletPool.GetObject();
        }
    }
}