using Actions.CubeActions;
using Actions.Shoot;
using Configs;
using Infrastructure;
using UnityEngine;

namespace Actions
{
    public class ActionsService : MonoBehaviour
    {
        [SerializeField] private CubeService _cubeService;
        [SerializeField] private ShootService _shootService;

        private MessageBus _messageBus;
        private GameConfig _gameConfig;

        private GameState _currentState;

        public void Init(MessageBus messageBus, GameConfig gameConfig)
        {
            _messageBus = messageBus;
            _gameConfig = gameConfig;

            _messageBus.OnGameStateChanged += ChangeGameState;
            _shootService.OnTryGetNewTarget += TryGetNewTarget;
            _shootService.OnDestroyCubeByHit += DestroyCube;
            
            _shootService.OnStopAction += StopCurrentAction;
            _cubeService.OnStopAction += StopCurrentAction;

            _shootService.Init(_messageBus, _gameConfig);
            _cubeService.Init(_messageBus, _gameConfig);
        }

        private void DestroyCube(int cubeID)
        {
            _cubeService.DestroyCubeByID(cubeID);
        }

        private void TryGetNewTarget()
        {
            var targetCube = _cubeService.GetRandomCube();
            if (targetCube == null)
            {
                print("No cube to shoot");
                StopCurrentAction();
            }
            else
            {
                _shootService.TryToShoot(targetCube);
            }
        }

        private void ChangeGameState(GameState newState)
        {
            switch (newState)
            {
                case GameState.None:
                    BreakCurrentAction();
                    return;
                case GameState.Spawning:
                    _cubeService.SetSpawnState();
                    break;
                case GameState.Moving:
                    _cubeService.SetMoveState();
                    break;
                case GameState.Shooting:
                    if (_gameConfig.IsMoveWhileShoot) _cubeService.SetMoveState();
                    _shootService.PrepareToShoot();
                    break;
                case GameState.Eating:
                    _cubeService.SetEatState();
                    break;
            }
            _currentState = newState;
        }

        private void StopCurrentAction()
        {
            _messageBus.OnActionEnded?.Invoke();
        }

        private void BreakCurrentAction()
        {
            switch (_currentState)
            {
                case GameState.Moving:
                    _cubeService.StopAction(_currentState);
                    break;
                case GameState.Eating:
                    _cubeService.StopAction(_currentState);
                    _messageBus.OnGameStateChanged?.Invoke(GameState.Moving);
                    return;
                case GameState.Shooting:
                    _shootService.StopAction();
                    if (_gameConfig.IsMoveWhileShoot) 
                        _messageBus.OnGameStateChanged?.Invoke(GameState.Moving);
                    break;
            }
        }
    }

    public enum GameState
    {
        None,
        Spawning,
        Moving,
        Shooting,
        Eating,
    }
}