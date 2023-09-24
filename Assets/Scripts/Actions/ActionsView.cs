using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Actions
{
    public class ActionsView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _stateText;
        
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _stopActionButton;
    
        [SerializeField] private Button _spawnButton;
        [SerializeField] private Button _moveButton;
        [SerializeField] private Button _shootButton;
        [SerializeField] private Button _eatButton;
    
        private MessageBus _messageBus;

        public void Init(MessageBus messageBus)
        {
            _messageBus = messageBus;
            _messageBus.OnGameStateChanged += UpdateView;
            _messageBus.OnActionEnded += () => UpdateView(GameState.None);
        
            _settingsButton.onClick.AddListener(()=>    _messageBus.OnSwitchSettingsPanel?.Invoke(true));
            _stopActionButton.onClick.AddListener(() => _messageBus.OnGameStateChanged?.Invoke(GameState.None));
            _spawnButton.onClick.AddListener(()=>       _messageBus.OnGameStateChanged?.Invoke(GameState.Spawning));
            _moveButton.onClick.AddListener(()=>        _messageBus.OnGameStateChanged?.Invoke(GameState.Moving));
            _shootButton.onClick.AddListener(()=>       _messageBus.OnGameStateChanged?.Invoke(GameState.Shooting));
            _eatButton.onClick.AddListener(()=>         _messageBus.OnGameStateChanged?.Invoke(GameState.Eating));
        }

        private void UpdateView(GameState state)
        {
            _stateText.text = "Current State: " + state;
            _stopActionButton.interactable = !(state == GameState.None || state == GameState.Spawning);

            _spawnButton.interactable = !(state != GameState.Spawning && state != GameState.None);
            _moveButton.interactable = !(state != GameState.Spawning && state != GameState.None);
            _shootButton.interactable = !(state != GameState.Spawning && state != GameState.None);
            _eatButton.interactable = !(state != GameState.Spawning && state != GameState.None);
        }
    }
}