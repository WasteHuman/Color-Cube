using TMPro;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI.MainMenu.Leaderboard
{
    public class CurrentPlayer : MonoBehaviour
    {
        [Header("Player info")]
        [SerializeField] private Image _placeImage;
        [SerializeField] private TextMeshProUGUI _rankText;
        [SerializeField] private TextMeshProUGUI _playerNameText;
        [SerializeField] private TextMeshProUGUI _bestScoreText;

        [Space(10), Header("Leaderboard")]
        [SerializeField] private LeaderboardYG _ldYG;

        [Space(10), Header("Place style selector")]
        [SerializeField] private PlaceStyleSelector _placeStyleSelector;

        [SerializeField] private LBPlayerDataYG _playerDataYG;

        public void GetPlayerData()
        {
            _playerDataYG = _ldYG.FindCurrentPlayer();
        }

        private void SetPlaceView()
        {
            SetSpriteByRank();
        }

        private void SetEntries()
        {
            GetPlayerData();

            if (_playerDataYG != null)
            {
                _playerDataYG.UpdateEntries();
                _rankText.text = _playerDataYG.data.rank;
                _playerNameText.text = _playerDataYG.data.name;
                _bestScoreText.text = _playerDataYG.data.score;
            }
        }

        private void SetSpriteByRank()
        {
            if (_playerDataYG != null)
            {
                int.TryParse(_playerDataYG.data.rank, out int rank);

                _placeImage.sprite = _placeStyleSelector.GetSpriteByPlace(rank);
            }
        }

        public void UpdateEntries()
        {
            SetEntries();
            SetPlaceView();
        }
    }
}