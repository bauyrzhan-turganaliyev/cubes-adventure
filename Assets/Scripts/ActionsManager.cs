using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionsManager : MonoBehaviour
{
    [SerializeField] private Button _stopButton;
    [SerializeField] private Button _spawnButton;
    [SerializeField] private Button _moveButton;
    [SerializeField] private Button _shootButton;
    [SerializeField] private Button _eatButton;

    [SerializeField] private CubeService _cubeService;
    [SerializeField] private ShootService _shootService;

    [SerializeField] private Transform _bulletStart;

    public void Awake()
    {
        _cubeService.Init();
        
        _stopButton.onClick.AddListener(()=> ChangeGameCurrentAction(GameCurrentAction.None));
        _spawnButton.onClick.AddListener(()=> ChangeGameCurrentAction(GameCurrentAction.Spawning));
        _moveButton.onClick.AddListener(()=> ChangeGameCurrentAction(GameCurrentAction.Moving));
        _shootButton.onClick.AddListener(()=> ChangeGameCurrentAction(GameCurrentAction.Shooting));
        _eatButton.onClick.AddListener(()=> ChangeGameCurrentAction(GameCurrentAction.Eating));
    }

    private void ChangeGameCurrentAction(GameCurrentAction newAction)
    {
        switch (newAction)
        {
            case GameCurrentAction.None:
                break;
            case GameCurrentAction.Spawning:
                _cubeService.Spawn();
                break;
            case GameCurrentAction.Moving:
                _cubeService.Move();
                break;
            case GameCurrentAction.Shooting:
                _shootService.ShootBullet(_bulletStart.position, _cubeService.GetRandomCube());
                break;
            case GameCurrentAction.Eating:
                break;
        }
    }
}

public enum GameCurrentAction
{
    None,
    Spawning,
    Moving,
    Shooting,
    Eating,
}