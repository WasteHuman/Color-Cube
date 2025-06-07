using Gameplay.Player;
using TMPro;
using UnityEngine;

namespace MainMenu
{
    public class BestScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _bestScoreText;

        public void InitializeBestScore()
        {
            int bestScore = PlayerPrefs.GetInt(PlayerPrefsConsts.BEST_SCORE);

            _bestScoreText.text = $"{bestScore}";
        }
    }
}