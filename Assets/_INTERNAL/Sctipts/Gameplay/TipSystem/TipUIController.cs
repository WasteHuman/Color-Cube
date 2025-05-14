using UnityEngine.UI;

namespace Gameplay.TipSystem
{
    public class TipUIController
    {
        private readonly Image _activated;
        private readonly Image _cooldown;
        private readonly Button _button;

        public TipUIController(Image activated, Image cooldown, Button button)
        {
            _activated = activated;
            _cooldown = cooldown;
            _button = button;
        }

        public TipUIController(Image cooldownm, Button button)
        {
            _cooldown = cooldownm;
            _button = button;
        }

        public void SetCooldownProgress(float cooldown)
        {
            if (_cooldown != null)
                _cooldown.fillAmount = cooldown;
        }

        public void SetActivatedProgress(float activated)
        {
            if (_activated != null)
                _activated.fillAmount = activated;
        }

        public void SetButtonInteracteble(bool value) => _button.interactable = value;

        public void ShowActive(bool show)
        {
            if (_activated != null)
                _activated.enabled = show;
        }

        public void ShowCooldown(bool show)
        {
            if (_cooldown != null)
                _cooldown.enabled = show;
        }
    }
}