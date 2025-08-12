using UnityEngine;

namespace UI.MainMenu.Leaderboard
{
    [CreateAssetMenu(menuName = "Leaderboard/Place style selector", fileName = "Place style selector")]
    public class PlaceStyleSelector : ScriptableObject
    {
        [SerializeField] private Sprite _firstPlaceSprite;
        [SerializeField] private Sprite _secondPlaceSprite;
        [SerializeField] public Sprite _thirdPlaceSprite;
        [SerializeField] private Sprite _defaultPlaceSprite;

        public Sprite GetSpriteByPlace(int place)
        {
            return place switch
            {
                1 => _firstPlaceSprite,
                2 => _secondPlaceSprite,
                3 => _thirdPlaceSprite,
                _ => _defaultPlaceSprite
            };
        }
    }
}