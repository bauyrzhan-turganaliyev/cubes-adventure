using System;
using Configs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class GameSettingsView : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;
        
        [Header("General Settings")] 
        [SerializeField] private Toggle _isMoveWhileShoot;
    
        [Header("Cube Settings")] 
        [SerializeField] private TMP_Text _minSpawnText;
        [SerializeField] private TMP_Text _maxSpawnText; 
        [SerializeField] private Slider _minSpawnCountSlider;
        [SerializeField] private Slider _maxSpawnCountSlider;
    
        [SerializeField] private Slider _cubeSpeedSlider;
        [SerializeField] private TMP_Text _cubeSpeedText; 
    
        [Header("Shooting Settings")] 
        [SerializeField] private Toggle _autoShootToggle;
    
        [SerializeField] private Slider _bulletSpeedSlider;
        [SerializeField] private TMP_Text _bulletSpeedText; 
    
        [SerializeField] private Slider _bulletLifetimeSlider;
        [SerializeField] private TMP_Text _bulletLifetimeText;
    
        [Header("Other Settings")] 
        [SerializeField] private Button _saveButton;

        public Action<GameSettings> OnClose;
        public Action<GameSettings> OnSave;
    
        private GameSettings _gameSettings;

        public void Init()
        {
            _closeButton.onClick.AddListener(()=>OnClose?.Invoke(_gameSettings));
            
            _isMoveWhileShoot.onValueChanged.AddListener((isMove => SetupModification()));
            _minSpawnCountSlider.onValueChanged.AddListener((value)=>OnSliderValueChanged(GameSetting.MinSpawnCount, (int)value));
            _maxSpawnCountSlider.onValueChanged.AddListener((value)=>OnSliderValueChanged(GameSetting.MaxSpawnCount, (int)value));
            _cubeSpeedSlider.onValueChanged.AddListener((value)=>OnSliderValueChanged(GameSetting.CubeSpeed, (int)value));
            _bulletSpeedSlider.onValueChanged.AddListener((value)=>OnSliderValueChanged(GameSetting.BulletSpeed, (int)value));
            _bulletLifetimeSlider.onValueChanged.AddListener((value)=>OnSliderValueChanged(GameSetting.BulletLifetime, (int)value));
            _autoShootToggle.onValueChanged.AddListener((isAuto => SetupModification()));
        
            _saveButton.onClick.AddListener((() => OnSave?.Invoke(_gameSettings)));
        }

        private void OnSliderValueChanged(GameSetting gameSetting, int value)
        {
            switch (gameSetting)
            {
                case GameSetting.MinSpawnCount:
                    _minSpawnText.text = $"{value}";
                    break;
                case GameSetting.MaxSpawnCount:
                    _maxSpawnText.text = $"{value}";
                    break;
                case GameSetting.CubeSpeed:
                    _cubeSpeedText.text = value.ToString();
                    break;
                case GameSetting.BulletSpeed:
                    _bulletSpeedText.text = value.ToString();
                    break;
                case GameSetting.BulletLifetime:
                    _bulletLifetimeText.text = value.ToString();
                    break;
            }

            SetupModification();
        }

        private void SetupModification()
        {        
            _gameSettings = new GameSettings
            {
                IsMoveWhileShoot = _isMoveWhileShoot.isOn,
                MinSpawnCount = (int)_minSpawnCountSlider.value,
                MaxSpawnCount = (int)_maxSpawnCountSlider.value,
                CubeSpeed = (int)_cubeSpeedSlider.value,
                BulletSpeed = (int)_bulletSpeedSlider.value,
                BulletLifetime = (int)_bulletLifetimeSlider.value,
                AutoShoot = _autoShootToggle.isOn
            };
        }

        public void UpdateUI(GameConfig gameConfig)
        {
            _isMoveWhileShoot.isOn = gameConfig.IsMoveWhileShoot;
            
            _minSpawnText.text = gameConfig.MinSpawnCount.ToString();
            _maxSpawnText.text = gameConfig.MaxSpawnCount.ToString();
            _cubeSpeedText.text = gameConfig.CubeSpeed.ToString();
            _autoShootToggle.isOn = gameConfig.AutoShoot;
            _bulletSpeedText.text = gameConfig.BulletSpeed.ToString();
            _bulletLifetimeText.text = gameConfig.BulletLifetime.ToString();

            _minSpawnCountSlider.value = gameConfig.MinSpawnCount;
            _maxSpawnCountSlider.value = gameConfig.MaxSpawnCount;
            _cubeSpeedSlider.value = gameConfig.CubeSpeed;
            _bulletSpeedSlider.value = gameConfig.BulletSpeed;
            _bulletLifetimeSlider.value = gameConfig.BulletLifetime;
        }
    }
}