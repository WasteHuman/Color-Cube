using Gameplay.SoundsSystem;
using TMPro;
using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace GameWindows
{
    public class LoseWindow : SimpleWindow
    {
        [Space(10), Header("In-window buttons")]
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _goHomeButton;
        [SerializeField] private Button _someButton;

        [Space(10), Header("In-window text")]
        [SerializeField] private TextMeshProUGUI _currentScoreText;

        [Space(10), Header("Main menu scene")]
        [SerializeField] private SceneAsset _mainMenuScene;

        public AudioSystem AudioSystem => AudioSystem.Instance;

        private void OnEnable()
        {
            _resetButton.onClick.AddListener(ResetGame);
            _goHomeButton.onClick.AddListener(OnClosed);
        }

        private void OnDisable()
        {
            _resetButton.onClick.RemoveListener(ResetGame);
            _goHomeButton.onClick.RemoveListener(OnClosed);
        }

        private void ResetGame()
        {
            AudioSystem.PlaySoundByID(SoundID.Click);
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        public void DisplayCurrentScore(int score)
        {
            switch (YandexGame.lang)
            {
                case LanguageConsts.RU:
                    _currentScoreText.text = $"Счёт: {score}";
                    break;
                case LanguageConsts.EN:
                    _currentScoreText.text = $"Score: {score}";
                    break;
            }
        }

        protected override void OnClosed()
        {
            AudioSystem.PlaySoundByID(SoundID.Click);
            SceneManager.LoadSceneAsync(_mainMenuScene.name);
        }
    }
}