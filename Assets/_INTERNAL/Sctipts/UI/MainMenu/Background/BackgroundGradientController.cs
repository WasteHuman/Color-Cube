using Core.Data;
using System.Collections.Generic;
using UnityEngine;

namespace UI.MainMenu.Background
{
    public class BackgroundGradientController : MonoBehaviour
    {
        [Header("Pressets")]
        [SerializeField] private List<GradientVariant> _backgroundPresets;

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

        private void Awake()
        {
            LoadPresset();
        }

        private void SubscribeOnPressetEvents()
        {
            foreach (GradientVariant presset in _backgroundPresets)
            {
                presset.OnPressetSelected += HandleSellectedPreset;
            }
        }

        private void UnsubscribeFromPressetEvents()
        {
            foreach (GradientVariant presset in _backgroundPresets)
            {
                presset.OnPressetSelected -= HandleSellectedPreset;
            }
        }

        private void LoadPresset()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsConsts.BG_PRESET_NAME))
            {
                string presetName = PlayerPrefs.GetString(PlayerPrefsConsts.BG_PRESET_NAME);
                foreach (GradientVariant variant in _backgroundPresets)
                {
                    if (variant.Preset.name == presetName)
                    {
                        _skybox.material = variant.Preset.PresetMaterial;
                        Debug.Log($"Loaded preset: {presetName}");
                        return;
                    }
                }
            }
        }

        private void HandleSellectedPreset(Material material)
        {
            _skybox.material = material;
        }
    }
}