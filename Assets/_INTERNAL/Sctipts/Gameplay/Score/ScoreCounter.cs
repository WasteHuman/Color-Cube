using Gameplay.Player;
using System;
using UnityEngine;
using YG;

namespace Gameplay.Score
{
    public class ScoreCounter
    {
        private int _currentScore;
        private int _bestScore;

        private readonly bool _isDebug;

        public int Score => _currentScore;

        public event Action<int> ScoreChanged;
        public event Action<int> BestScoreUpdated;

        public ScoreCounter(bool debug)
        {
            _currentScore = 0;

            _isDebug = debug;
        }

        private void BestScoreInitialize()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsConsts.BEST_SCORE))
            {
                _bestScore = PlayerPrefs.GetInt(PlayerPrefsConsts.BEST_SCORE);
                return;
            }

            _bestScore = 0;
            PlayerPrefs.SetInt(PlayerPrefsConsts.BEST_SCORE, _bestScore);
        }

        public void Initialize()
        {
            if(_isDebug)
                PlayerPrefs.DeleteKey(PlayerPrefsConsts.BEST_SCORE);

            BestScoreInitialize();

            ScoreChanged?.Invoke(_currentScore);
            BestScoreUpdated?.Invoke(_currentScore);
        }

        public void Add()
        {
            _currentScore++;
            RecordNewBest();
            ScoreChanged?.Invoke(Score);
        }

        public int GetBestScore()
        {
            return _bestScore;
        }

        public void RecordNewBest()
        {
            if (_currentScore > _bestScore)
            {
                _bestScore = _currentScore;

                PlayerPrefs.SetInt(PlayerPrefsConsts.BEST_SCORE, _bestScore);
                PlayerPrefs.Save();

                BestScoreUpdated?.Invoke(_bestScore);
            }
        }

        public void LoadScore(int score)
        {
            _currentScore = score;
        }
    }
}