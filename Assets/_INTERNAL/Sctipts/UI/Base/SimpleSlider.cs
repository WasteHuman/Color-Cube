using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class SimpleSlider : MonoBehaviour
    {
        [SerializeField] private Image _fill;
        [SerializeField] private float _value;

        private float _minValue;
        private float _maxValue;

        public float Value => _value;
        public float MinValue => _minValue;
        public float MaxValue => _maxValue;

        public void SetDefaultAmount()
        {
            _fill.fillAmount = 1f;
        }

        public void SetValue(float currentTime, float timerDuration)
        {
            _value = currentTime;
            Visualization(currentTime, timerDuration);
        }

        private void Visualization(float currentTime, float timerDuration)
        {
            _fill.fillAmount = currentTime / timerDuration;
        }
    }
}