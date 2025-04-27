using TMPro;
using UI;
using UnityEngine;
using YG;

namespace Gameplay.Score
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _currentScore;
        [SerializeField] private TextMeshProUGUI _bestScore;

        [Space(10), Header("Debug")]
        [Tooltip("Если включено, то удаляет ключ BEST_SCORE")] 
        [SerializeField] private bool _isDebug;

        private ScoreCounter _scoreCounter;

        public ScoreCounter ScoreCounter => _scoreCounter;

        private void OnEnable()
        {
            Initialization();

            _scoreCounter.ScoreChanged += OnScoreChanged;
            _scoreCounter.BestScoreUpdated += OnBestScoreUpdated;
        }

        private void OnDisable()
        {
            _scoreCounter.ScoreChanged -= OnScoreChanged;
            _scoreCounter.BestScoreUpdated -= OnBestScoreUpdated;
        }

        public void Initialization()
        {
            _scoreCounter = new(_isDebug);
            _scoreCounter.Initialize();

            OnScoreChanged(_scoreCounter.Score);
            OnBestScoreUpdated(_scoreCounter.GetBestScore());
        }

        private void OnScoreChanged(int score)
        {
            if (YandexGame.lang == LanguageConsts.RU)
            {
                _currentScore.text = $"Тек. счёт: {score}";
            }
            else if (YandexGame.lang == LanguageConsts.EN)
            {
                _currentScore.text = $"Current score: {score}";
            }
        }

        private void OnBestScoreUpdated(int best)
        {
            if (YandexGame.lang == LanguageConsts.RU)
            {
                _bestScore.text = $"Лучший счёт: {best}";
            }
            else if (YandexGame.lang == LanguageConsts.EN)
            {
                _bestScore.text = $"Best score: {best}";
            }
        }
    }
}