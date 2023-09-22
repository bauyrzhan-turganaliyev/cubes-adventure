using UnityEditor.VersionControl;
using UnityEngine;

public class GameSettingsService : MonoBehaviour
{
    [SerializeField] private GameSettingsView _gameSettingsView;
    [SerializeField] private SettingsApprovalView _settingsApprovalView;

    private GameConfig _gameConfig;
    private GameSettings _gameSettings;
    private MessageBus _messageBus;

    public void Init(GameConfig gameConfig, MessageBus messageBus)
    {
        _gameConfig = gameConfig;
        _messageBus = messageBus;

        _messageBus.OnSwitchSettingsPanel += SwitchSettingsPanel;

        _gameSettingsView.OnSave += gameSettings =>
        {
            _gameSettings = gameSettings;
            SaveSettings();
        };
        _gameSettingsView.OnClose += gameSettings =>
        {
            _gameSettings = gameSettings;
            if (_gameSettings == null)
            {
               SwitchSettingsPanel(false);
               return;
            }
            if (IsHaveChanges()) SwitchApprovalPanel(true);
            else SwitchSettingsPanel(false);
        };

        _settingsApprovalView.OnReturn += () =>
        {
            SwitchApprovalPanel(false);
        };
        
        _settingsApprovalView.OnSave += () =>
        {
            SaveSettings();
            SwitchSettingsPanel(false);
            SwitchApprovalPanel(false);
        };
        
        _settingsApprovalView.OnQuit += () =>
        {
            SwitchSettingsPanel(false);
            SwitchApprovalPanel(false);
        };
        
        _gameSettingsView.Init();
        _settingsApprovalView.Init();
    }

    private void SaveSettings()
    {
        _gameConfig.SpawnCount = _gameSettings.SpawnCount;
        _gameConfig.CubeSpeed = _gameSettings.CubeSpeed;
        _gameConfig.BulletSpeed = _gameSettings.BulletSpeed;
        _gameConfig.BulletLifetime = _gameSettings.BulletLifetime;
        _gameConfig.AutoShoot = _gameSettings.AutoShoot;
    }

    public void SwitchSettingsPanel(bool isActive)
    {
        _gameSettingsView.gameObject.SetActive(isActive);
    }    
    private void SwitchApprovalPanel(bool isActive)
    {
        _settingsApprovalView.gameObject.SetActive(isActive);
    }

    private bool IsHaveChanges()
    {
        if (_gameConfig.SpawnCount != _gameSettings.SpawnCount) return true;
        if (_gameConfig.CubeSpeed != _gameSettings.CubeSpeed) return true;
        if (_gameConfig.BulletSpeed != _gameSettings.BulletSpeed) return true;
        if (_gameConfig.BulletLifetime != _gameSettings.BulletLifetime) return true;
        if (_gameConfig.AutoShoot != _gameSettings.AutoShoot) return true;

        return false;
    }
}

public enum SliderSetting
{
    SpawnCount,
    CubeSpeed,
    BulletSpeed,
    BulletLifetime,
}