using System.Collections.Generic;
using UnityEngine;

namespace MainMenu.Background
{
    public class BackgroundGradientController : MonoBehaviour
    {
        [Header("Pressets")]
        [SerializeField] private List<GradientVariant> _backgroundPressets;

        [Space(10), Header("Skybox")]
        [SerializeField] private Skybox _skybox;

        private void OnEnable()
        {
            SubscribeOnPressetEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromPressetEvents();
        }

        private void SubscribeOnPressetEvents()
        {
            foreach (GradientVariant presset in _backgroundPressets)
            {
                presset.OnPressetSelected += HandleSellectedPresset;
            }
        }

        private void UnsubscribeFromPressetEvents()
        {
            foreach (GradientVariant presset in _backgroundPressets)
            {
                presset.OnPressetSelected -= HandleSellectedPresset;
            }
        }

        private void HandleSellectedPresset(Material material)
        {
            _skybox.material = material;
        }
    }
}