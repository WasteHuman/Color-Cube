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

        [Space(10), Header("Cubes")]
        [SerializeField] private List<MainCubeView> _mainCubeViews;

        private void Awake()
        {
            if (Time.timeScale == 0f)
                Time.timeScale = 1f;

            _bestScoreView.InitializeBestScore();
            _walletView.Initialization();
            MainCubesInitialization();

            _leaderboardHolder.Initialization();

            Debug.Log($"Main menu is initialized");
        }

        private void MainCubesInitialization()
        {
            foreach(MainCubeView cube in _mainCubeViews)
            {
                cube.Initialization();
            }
        }
    }
}