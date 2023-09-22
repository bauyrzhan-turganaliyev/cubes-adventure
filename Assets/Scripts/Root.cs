using UnityEditor.VersionControl;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private GameSettingsService _gameSettingsService;
    
    [SerializeField] private ActionsManager _actionsManager;
    [SerializeField] private UIService _uiService;
    
    private MessageBus _messageBus;

    public void Awake()
    {
        Application.targetFrameRate = 60;

        _messageBus = new MessageBus();
        _gameSettingsService.Init(_gameConfig, _messageBus);
        _actionsManager.Init(_messageBus);
        _uiService.Init(_messageBus);
    }
}