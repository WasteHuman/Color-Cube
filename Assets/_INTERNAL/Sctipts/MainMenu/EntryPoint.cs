using Gameplay.SoundsSystem;
using MainMenu.Background;
using MainMenu.Leaderboard;
using System.Collections.Generic;
using UnityEngine;

namespace MainMenu
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Main menu Systems")]
        [SerializeField] private LeaderboardHolder _leaderboardHolder;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private BestScoreView _bestScoreView;
        [SerializeField] private BackgroundColorAnimation _backgroundColorAnimation;

        [Space(10), Header("Cubes")]
        [SerializeField] private List<MainCubeView> _mainCubeViews;

        private void Awake()
        {
            if (Time.timeScale == 0f)
                Time.timeScale = 1f;

            Initializations();
        }

        private void Initializations()
        {
            _bestScoreView.InitializeBestScore();
            _walletView.Initialization();
            MainCubesInitialization();
            _backgroundColorAnimation.Initialization();

            _leaderboardHolder.Initialization();

            PlayMusic();
        }

        private void MainCubesInitialization()
        {
            foreach(MainCubeView cube in _mainCubeViews)
            {
                cube.Initialization();
            }
        }

        private void PlayMusic()
        {
            AudioSystem.Instance.LoadVolumes();
            AudioSystem.Instance.PlayMusicByID(SoundID.MainTheme);
        }
    }
}