using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameSettingsView : MonoBehaviour
{
    [SerializeField] private Button _closeButton;
    
    [Header("Cube Settings")] 
    [SerializeField] private Slider _spawnCountSlider;
    [SerializeField] private TMP_Text _spawnCountText; 
    
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
        _spawnCountSlider.onValueChanged.AddListener((value)=>OnSliderValueChanged(SliderSetting.SpawnCount, (int)value));
        _cubeSpeedSlider.onValueChanged.AddListener((value)=>OnSliderValueChanged(SliderSetting.CubeSpeed, (int)value));
        _bulletSpeedSlider.onValueChanged.AddListener((value)=>OnSliderValueChanged(SliderSetting.BulletSpeed, (int)value));
        _bulletLifetimeSlider.onValueChanged.AddListener((value)=>OnSliderValueChanged(SliderSetting.BulletLifetime, (int)value));
        _autoShootToggle.onValueChanged.AddListener((isAuto => SetupModification()));
        
        _saveButton.onClick.AddListener((() => OnSave?.Invoke(_gameSettings)));
    }

    private void OnSliderValueChanged(SliderSetting sliderSetting, int value)
    {
        switch (sliderSetting)
        {
            case SliderSetting.SpawnCount:
                _spawnCountText.text = value.ToString();
                break;
            case SliderSetting.CubeSpeed:
                _cubeSpeedText.text = value.ToString();
                break;
            case SliderSetting.BulletSpeed:
                _bulletSpeedText.text = value.ToString();
                break;
            case SliderSetting.BulletLifetime:
                _bulletLifetimeText.text = value.ToString();
                break;
        }

        SetupModification();
    }

    private void SetupModification()
    {        
        _gameSettings = new GameSettings
        {
            SpawnCount = (int)_spawnCountSlider.value,
            CubeSpeed = (int)_cubeSpeedSlider.value,
            BulletSpeed = (int)_bulletSpeedSlider.value,
            BulletLifetime = (int)_bulletLifetimeSlider.value,
            AutoShoot = _autoShootToggle.isOn
        };
    }
}