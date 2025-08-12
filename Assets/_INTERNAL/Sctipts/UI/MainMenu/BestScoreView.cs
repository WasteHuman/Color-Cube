using Core.Data;
using TMPro;
using UnityEngine;

namespace UI
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