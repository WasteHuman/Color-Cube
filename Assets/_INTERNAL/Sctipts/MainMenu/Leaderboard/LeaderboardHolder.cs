using Gameplay.Player;
using Gameplay.SoundsSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace MainMenu.Leaderboard
{
    public class LeaderboardHolder : MonoBehaviour
    {
        [Header("Leaderboard")]
        [SerializeField] private GameObject _leaderBoardPanel;
        [SerializeField] private LeaderboardYG _leaderboardYG;

        [Space(10), Header("Buttons")]
        [SerializeField] private Button _leaderBoardButton;
        [Header("Other buttons")]
        [SerializeField] private List<Button> _otherButtons;

        private AudioSystem _audioSystem;

        private void OnDisable()
        {
            _leaderBoardButton.onClick.RemoveListener(ToggleLeaderboard);
        }

        public void Initialization()
        {
            _leaderBoardButton.onClick.AddListener(ToggleLeaderboard);
            int bestScore = PlayerPrefs.GetInt(PlayerPrefsConsts.BEST_SCORE);
            _leaderboardYG.NewScore(bestScore);

            _audioSystem = AudioSystem.Instance;
        }

        private void ToggleLeaderboard()
        {
            _audioSystem.PlaySoundByID(SoundID.Click);
            _leaderBoardPanel.SetActive(!_leaderBoardPanel.activeSelf);
            ToggleOtherButtons();
        }

        private void ToggleOtherButtons()
        {
            foreach (Button button in _otherButtons)
            {
                button.interactable = !button.interactable;
            }
        }
    }
}