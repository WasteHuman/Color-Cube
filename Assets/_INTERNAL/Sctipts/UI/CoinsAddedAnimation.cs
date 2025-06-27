using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class CoinsAddedAnimation : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        [Space(10), Header("Animation settings")]
        [SerializeField] private float _speed;
        [SerializeField] private float _delayBeforeDestroy;

        private Vector3 _startPosition;
        private bool _isEnabling = false;
        private float _fadeTime;

        public event Action<CoinsAddedAnimation> TimeEnded;

        private void OnEnable()
        {
            SetDefaultState();
            _isEnabling = true;
        }

        private void OnDisable()
        {
            _isEnabling = false;
            SetDefaultState();
        }

        private void Awake()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            if(_isEnabling)
                MoveUp();
        }

        private void MoveUp()
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.up);
            Fade();
        }

        private void Fade()
        {
            _fadeTime -= Time.deltaTime;

            float duration = _fadeTime / _delayBeforeDestroy;

            _text.alpha = duration;

            if (_fadeTime <= 0f)
                TimeEnded?.Invoke(this);
        }

        private void SetFadeDuration()
        {
            _fadeTime = _delayBeforeDestroy;
        }

        private void SetDefaultState()
        {
            SetFadeDuration();

            transform.position = _startPosition;
            _startPosition = transform.position;
        }

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}