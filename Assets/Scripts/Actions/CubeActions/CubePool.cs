using System.Collections.Generic;
using UnityEngine;

namespace Actions.CubeActions
{
    public class CubePool
    {
        private ObjectPool<Cube> cubePool;

        public CubePool(Cube prefab, Transform parent, int initialSize = 10)
        {
            cubePool = new ObjectPool<Cube>(
                prefab,
                parent,
                () =>
                {
                    Cube cube = Object.Instantiate(prefab, parent);
                    cube.Init();
                    return cube;
                },
                initialSize
            );
        }

        public Cube GetCube()
        {
            return cubePool.GetObject();
        }
    }
}