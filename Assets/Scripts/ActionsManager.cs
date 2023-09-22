using System;
using UnityEngine;
using UnityEngine.UI;

public class ActionsManager : MonoBehaviour
{
    [SerializeField] private CubeService _cubeService;
    [SerializeField] private ShootService _shootService;

    [SerializeField] private Transform _bulletStart;
    private MessageBus _messageBus;

    public void Init(MessageBus messageBus)
    {
        _messageBus = messageBus;

        _messageBus.OnChangeGameAction += ChangeGameCurrentAction;
        
        _cubeService.Init();
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
                _shootService.ShootBullet(_bulletStart.position, _cubeService.GetRandomCube().transform.position);
                break;
            case GameCurrentAction.Eating:
                _cubeService.Eat();
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