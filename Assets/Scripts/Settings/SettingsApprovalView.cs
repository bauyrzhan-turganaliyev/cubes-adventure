using System;
using UnityEngine;
using UnityEngine.UI;

namespace Settings
{
    public class SettingsApprovalView : MonoBehaviour
    {
        [SerializeField] private Button _returnButton;
        [SerializeField] private Button _saveButton;
        [SerializeField] private Button _quitButton;

        public Action OnReturn;
        public Action OnSave;
        public Action OnQuit;
    
        public void Init()
        {
            _returnButton.onClick.AddListener((() => OnReturn?.Invoke()));
            _saveButton.onClick.AddListener((() => OnSave?.Invoke()));
            _quitButton.onClick.AddListener((() => OnQuit?.Invoke()));
        }
    }
}