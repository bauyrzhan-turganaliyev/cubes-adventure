using Actions;
using Configs;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;

namespace Infrastructure
{
    public class Root : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private GameSettingsService _gameSettingsService;
    
        [SerializeField] private ActionsService _actionsService;
        [SerializeField] private ActionsView _actionsView;
    
        private MessageBus _messageBus;

        public void Awake()
        {
            Application.targetFrameRate = 60;

            _messageBus = new MessageBus();
            _gameSettingsService.Init(_messageBus, _gameConfig);
            _actionsView.Init(_messageBus);
            _actionsService.Init(_messageBus, _gameConfig);
        }
    }
}