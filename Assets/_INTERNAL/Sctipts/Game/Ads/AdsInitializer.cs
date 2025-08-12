using Sctipts.Game.Ads;
using UnityEngine;

namespace Game.Ads
{
    public class AdsInitializer : MonoBehaviour
    {
        private void Awake()
        {
            AdService.Initialize();
        }
    }
}