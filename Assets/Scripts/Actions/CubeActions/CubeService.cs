using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using Infrastructure;
using UnityEngine;
using Random = System.Random;

namespace Actions.CubeActions
{
    public class CubeService : MonoBehaviour
    {
        [SerializeField] private CubeSpawner _cubeSpawner;
        [SerializeField] private CubeMovement _cubeMovement;
        [SerializeField] private CubeEatService _cubeEatService;

        public Action OnStopAction;
        
        private Dictionary<int, Cube> _cubes;
        private MessageBus _messageBus;
        private GameConfig _gameConfig;
        
        public void Init(MessageBus messageBus, GameConfig gameConfig)
        {
            _messageBus = messageBus;
            _gameConfig = gameConfig;

            _messageBus.OnSettingsChanged += SetupConfigs;
            _cubeEatService.OnAteCube += DestroyCubeByID;
            _cubeEatService.OnStopEating += () => OnStopAction?.Invoke();

            _cubes = new  Dictionary<int, Cube>();

            SetupConfigs();

            _cubeSpawner.Init();
            _cubeMovement.Init();
        }

        private void SetupConfigs()
        {
            _cubeSpawner.MinSpawnCount = _gameConfig.MinSpawnCount;
            _cubeSpawner.MaxSpawnCount = _gameConfig.MaxSpawnCount;
            _cubeMovement.MoveSpeed = _gameConfig.CubeSpeed;
        }
        public void StopAction(GameState state)
        {
            switch (state)
            {
                case GameState.Moving:
                    _cubeMovement.StopMoving();
                    break;
                case GameState.Eating:
                    _cubeEatService.StopEating();
                    break;
            }
        }
        public void SetSpawnState()
        {
            Clear();
        
            _cubes = _cubeSpawner.SpawnCubes();

            RegisterCubes();

            _messageBus.OnCubesInitialized?.Invoke(_cubes);
            _messageBus.OnActionEnded?.Invoke();
        }
    
        public void SetMoveState()
        {
            _cubeMovement.StartMoving();
        }

        public void SetEatState()
        {
            _cubeMovement.StartMoving();
            _cubeEatService.SetEaterCube(GetRandomCube());
            _cubeEatService.SetTarget();
        }

        public Cube GetRandomCube()
        {
            if (_cubes.Count == 0) return null;
            
            Random rand = new Random();
            return _cubes.ElementAt(rand.Next(0, _cubes.Count)).Value;
        }

        public void DestroyCubeByID(int cubeID)
        {
            var cube = _cubes[cubeID];
            if (cube != null)
            {
                cube.Destroy();
            }
        }
        private void RegisterCubes()
        {
            foreach (var pair in _cubes)
            {
                Cube cube = pair.Value;
                cube.OnDestroyed += RemoveCubeByIndex;
            }

            _cubeMovement.SetCubes(_cubes);
            _cubeEatService.SetCubes(_cubes);
        }
        private void RemoveCubeByIndex(int id)
        {
            _cubes.Remove(id);
        }
        private void Clear()
        {
            foreach (var cube in _cubes)
            {
                cube.Value.Deactivate();
            }
        
            _cubes.Clear();
        }


    }
}