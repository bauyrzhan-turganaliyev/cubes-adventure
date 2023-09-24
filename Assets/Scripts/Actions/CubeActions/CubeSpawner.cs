using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Actions.CubeActions
{
    public class CubeSpawner : MonoBehaviour
    {
        public int MinSpawnCount { get; set; }
        public int MaxSpawnCount { get; set; }
    
        [SerializeField] private Cube _cubePrefab;
        [SerializeField] private Transform _spawnZone;
        [SerializeField] private Transform _parent;

        private Vector3 _spawnerPosition;
        private Vector3 _spawnerScale;
        
        private CubePool _cubePool;

        public void Init()
        {
            _spawnerPosition = _spawnZone.position;
            _spawnerScale = _spawnZone.localScale;
            _cubePool = new CubePool(_cubePrefab, _parent);
        }
    
        public Dictionary<int, Cube> SpawnCubes()
        {
            var cubes = new Dictionary<int, Cube>();
            var numberOfCubes = Random.Range(MinSpawnCount, MaxSpawnCount);
        
            for (int i = 0; i < numberOfCubes; i++)
            {
                var cubeID = i + 1;
                var cube = _cubePool.GetCube();
                cube.SetNumber(cubeID);
                cubes.Add(cubeID, cube);

                var spawnPosition = GetRandomPosition();
                cube.transform.position = spawnPosition;
            }

            return cubes;
        }

        private Vector3 GetRandomPosition()
        {
            Vector3 spawnPosition = new Vector3(
                Random.Range(_spawnerPosition.x - _spawnZone.localScale.x / 2,
                    _spawnerPosition.x + _spawnerScale.x / 2),
                Random.Range(1, 3),
                Random.Range(_spawnerPosition.z - _spawnerScale.z / 2,
                    _spawnerPosition.z + _spawnerScale.z / 2)
            );
            return spawnPosition;
        }
    }
}
