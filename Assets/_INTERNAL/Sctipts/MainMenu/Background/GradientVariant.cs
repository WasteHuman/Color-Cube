using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainMenu.Background
{
    public class GradientVariant : MonoBehaviour
    {
        [SerializeField] private BackgroundPresset _presset;

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
            OnPressetSelected?.Invoke(_presset.PressetMaterial);
        }
    }
}