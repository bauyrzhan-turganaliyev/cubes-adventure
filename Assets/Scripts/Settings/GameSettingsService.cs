using Configs;
using Infrastructure;
using UnityEngine;

namespace Settings
{
    public class GameSettingsService : MonoBehaviour
    {
        [SerializeField] private GameSettingsView _gameSettingsView;
        [SerializeField] private SettingsApprovalView _settingsApprovalView;

        private MessageBus _messageBus;
        private GameConfig _gameConfig;
        private GameSettings _gameSettings;

        public void Init(MessageBus messageBus, GameConfig gameConfig)
        {
            _messageBus = messageBus;
            _gameConfig = gameConfig;

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

            _gameSettingsView.UpdateUI(_gameConfig);
        }

        private void SaveSettings()
        {
            _gameConfig.IsMoveWhileShoot = _gameSettings.IsMoveWhileShoot;
            _gameConfig.MinSpawnCount = _gameSettings.MinSpawnCount;
            _gameConfig.MaxSpawnCount = _gameSettings.MaxSpawnCount;
            _gameConfig.CubeSpeed = _gameSettings.CubeSpeed;
            _gameConfig.BulletSpeed = _gameSettings.BulletSpeed;
            _gameConfig.BulletLifetime = _gameSettings.BulletLifetime;
            _gameConfig.AutoShoot = _gameSettings.AutoShoot;

            _messageBus.OnSettingsChanged?.Invoke();
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
            if (_gameConfig.IsMoveWhileShoot != _gameSettings.IsMoveWhileShoot) return true;
            if (_gameConfig.MinSpawnCount != _gameSettings.MinSpawnCount) return true;
            if (_gameConfig.MaxSpawnCount != _gameSettings.MaxSpawnCount) return true;
            if (_gameConfig.CubeSpeed != _gameSettings.CubeSpeed) return true;
            if (_gameConfig.BulletSpeed != _gameSettings.BulletSpeed) return true;
            if (_gameConfig.BulletLifetime != _gameSettings.BulletLifetime) return true;
            if (_gameConfig.AutoShoot != _gameSettings.AutoShoot) return true;

            return false;
        }
    }

    public enum GameSetting
    {
        MinSpawnCount,
        MaxSpawnCount,
        CubeSpeed,
        BulletSpeed,
        BulletLifetime,
    }
}