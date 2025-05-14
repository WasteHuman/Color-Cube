using Gameplay.Player;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class BestScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _bestScoreText;

        private void Start()
        {
            InitializeBestScore();
        }

        private void InitializeBestScore()
        {
            int bestScore = PlayerPrefs.GetInt(PlayerPrefsConsts.BEST_SCORE);

            _bestScoreText.text = $"{bestScore}";
        }
    }
}