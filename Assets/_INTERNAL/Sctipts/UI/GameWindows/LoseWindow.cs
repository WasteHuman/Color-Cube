using Game.Ads.FullscreenAdSystem;
using Game.SoundsSystem;
using Other;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace UI.GameWindows
{
    public class LoseWindow : SimpleWindow
    {
        [Space(10), Header("In-window buttons")]
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _goHomeButton;
        [field: SerializeField] public Button ShowRewardedAdButton { get; private set; }

        [Space(10), Header("In-window text")]
        [SerializeField] private TextMeshProUGUI _currentScoreText;
        [field: SerializeField] public TextMeshProUGUI ShowedAdCounter {  get; private set; }

        [Space(10), Header("Main menu _scene")]
        [SerializeField] private GameScene _mainMenuScene;

        [Space(10), Header("Animation")]
        [SerializeField] private LoseWindowAnimation _animation;

        private FullscreenAdMono _adMono;

        public AudioSystem AudioSystem => AudioSystem.Instance;

        public event Action OnGameRestared;

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

        private void Awake()
        {
            _animation.GetAnimator();
            _animation.OpenWindow();
        }

        private void ResetGame()
        {
            AudioSystem.PlaySoundByID(SoundID.Click);
            _adMono.Show();
            OnGameRestared?.Invoke();
        }

        public void InitializeFullscreenAdMono(FullscreenAdMono adMono)
        {
            _adMono = adMono;
        }

        public void DisplayCurrentScore(int score)
        {
            switch (YG2.lang)
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
            SceneManager.LoadSceneAsync(_mainMenuScene.SceneName);
            YG2.GameplayStop();
        }
    }
}