using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _resestButton;
    [SerializeField] private Button _stopActionButton;
    
    [SerializeField] private Button _spawnButton;
    [SerializeField] private Button _moveButton;
    [SerializeField] private Button _shootButton;
    [SerializeField] private Button _eatButton;
    
    private MessageBus _messageBus;

    public void Init(MessageBus messageBus)
    {
        _messageBus = messageBus;
        
        _settingsButton.onClick.AddListener(()=>    _messageBus.OnSwitchSettingsPanel?.Invoke(true));
        _stopActionButton.onClick.AddListener(() => _messageBus.OnChangeGameAction?.Invoke(GameCurrentAction.None));
        _spawnButton.onClick.AddListener(()=>       _messageBus.OnChangeGameAction?.Invoke(GameCurrentAction.Spawning));
        _moveButton.onClick.AddListener(()=>        _messageBus.OnChangeGameAction?.Invoke(GameCurrentAction.Moving));
        _shootButton.onClick.AddListener(()=>       _messageBus.OnChangeGameAction?.Invoke(GameCurrentAction.Shooting));
        _eatButton.onClick.AddListener(()=>         _messageBus.OnChangeGameAction?.Invoke(GameCurrentAction.Eating));
    }
}