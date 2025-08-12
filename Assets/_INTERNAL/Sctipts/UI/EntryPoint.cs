using Game.SoundsSystem;
using System.Collections.Generic;
using UI.MainMenu.Leaderboard;
using UnityEngine;

namespace UI
{
    public class EntryPoint : MonoBehaviour
    {
        [Header("Main menu Systems")]
        [SerializeField] private LeaderboardHolder _leaderboardHolder;
        [SerializeField] private WalletView _walletView;
        [SerializeField] private BestScoreView _bestScoreView;

        [Space(10), Header("Cubes")]
        [SerializeField] private List<MainCubeView> _mainCubeViews;

        private LeaderboardCleaner _leaderboardCleaner;
        private bool _isMusicPlaying;

        private void Awake()
        {
            ResetTimeScale();

            Initializations();
        }

        private void Initializations()
        {
            _bestScoreView.InitializeBestScore();
            _walletView.Initialization();
            MainCubesInitialization();

            _leaderboardHolder.Initialization();
            _leaderboardCleaner = new();
            _leaderboardCleaner.CleanLeaderboard(_leaderboardHolder.LeaderboardYG);

            if (!_isMusicPlaying)
                PlayMusic();
        }

        private void MainCubesInitialization()
        {
            foreach(MainCubeView cube in _mainCubeViews)
            {
                cube.Initialization();
            }
        }

        private void ResetTimeScale()
        {
            if (Time.timeScale != 1f)
            {
                Time.timeScale = 1f;
            }
        }

        private void PlayMusic()
        {
            _isMusicPlaying = true;

            AudioSystem.Instance.LoadVolumes();
            AudioSystem.Instance.PlayMusicByID(SoundID.MainTheme);
        }
    }
}