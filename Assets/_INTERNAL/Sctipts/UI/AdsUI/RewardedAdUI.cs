using TMPro;
using UnityEngine;

namespace UI.AdsUI
{
    public class RewardedAdUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _adCounter;

        public TextMeshProUGUI GetAdCounterText(TextMeshProUGUI adCounterText)
        {
            return _adCounter = adCounterText;
        }

        public void UpdateCounter(int count, int maxCount)
        {
            _adCounter.text = $"{count}/{maxCount}";
        }
    }
}