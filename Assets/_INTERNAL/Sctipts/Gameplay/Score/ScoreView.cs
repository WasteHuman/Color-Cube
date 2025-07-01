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

        private void SubscribeOnScoreChangesEvents()
        {
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

            SubscribeOnScoreChangesEvents();

            OnScoreChanged(_scoreCounter.Score);
            OnBestScoreUpdated(_scoreCounter.GetBestScore());
        }

        public void UpdateScoreText(int score)
        {
            _scoreCounter.LoadScore(score);

            switch (YandexGame.lang)
            {
                case LanguageConsts.RU:
                    _currentScore.text = $"Текущий счёт: {score}";
                    break;
                case LanguageConsts.EN:
                    _currentScore.text = $"Current score: {score}";
                    break;
            }

        }

        private void OnScoreChanged(int score)
        {
            switch (YandexGame.lang)
            {
                case LanguageConsts.RU:
                    _currentScore.text = $"Текущий счёт: {score}";
                    break;
                case LanguageConsts.EN:
                    _currentScore.text = $"Current score: {score}";
                    break;
            }
        }

        private void OnBestScoreUpdated(int best)
        {
            switch (YandexGame.lang)
            {
                case LanguageConsts.RU:
                    _bestScore.text = $"Лучший счёт: {best}";
                    break;
                case LanguageConsts.EN:
                    _bestScore.text = $"Best score: {best}";
                    break;
            }
        }
    }
}