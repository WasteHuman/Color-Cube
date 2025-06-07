using UnityEngine;
using UnityEngine.UI;
using YG;

namespace MainMenu.Leaderboard
{
    public class Place : MonoBehaviour
    {
        [SerializeField] private Image _placeImage;
        [SerializeField] private PlaceStyleSelector _placeStyleSelector;

        private LBPlayerDataYG _playerDataYG;

        private void Start()
        {
            GetPlayerDataYG();
            SetSpriteByRank();
        }

        private void GetPlayerDataYG()
        {
            _playerDataYG = GetComponent<LBPlayerDataYG>();
        }

        private void SetSpriteByRank()
        {
            int.TryParse(_playerDataYG.data.rank, out int rank);

            _placeImage.sprite = _placeStyleSelector.GetSpriteByPlace(rank);
        }
    }
}