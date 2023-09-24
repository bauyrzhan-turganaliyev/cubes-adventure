using System.Threading;
using System.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Actions.Shoot
{
    public class AnnounceView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TMP_Text _numberText;
        private CancellationTokenSource _cancellationTokenSource;

        public async void SetTargetNumber(int number)
        {
            // Cancel any ongoing animation or delay
            if (_cancellationTokenSource != null && !_cancellationTokenSource.Token.IsCancellationRequested)
            {
                _cancellationTokenSource.Cancel();
            }

            _cancellationTokenSource = new CancellationTokenSource();

            _numberText.text = number.ToString();

            Switch(true);

            try
            {
                // Delay for 1 second, but cancel if needed
                await Task.Delay(1000, _cancellationTokenSource.Token);
            }
            catch (TaskCanceledException)
            {
                // The delay was canceled, so we don't need to switch it off
                return;
            }

            Switch(false);
        }

        private void Switch(bool isOn)
        {
            _canvasGroup.DOFade(isOn ? 1 : 0, 0.4f);
        }
    }
}