using Gameplay.Player;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Background
{
    public class GradientVariant : MonoBehaviour
    {
        [field: SerializeField] public BackgroundPreset Preset { get; private set; }

        private Button _selectButton;

        public event Action<Material> OnPressetSelected;

        private void OnEnable()
        {
            GetButton();

            _selectButton.onClick.AddListener(HandleClickedButton);
        }

        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(HandleClickedButton);
        }

        private void GetButton()
        {
            if (_selectButton == null)
            {
                _selectButton = GetComponent<Button>();
            }
        }

        private void HandleClickedButton()
        {
            PlayerPrefs.SetString(PlayerPrefsConsts.BG_PRESET_NAME, Preset.name);
            PlayerPrefs.Save();

            OnPressetSelected?.Invoke(Preset.PresetMaterial);
        }
    }
}