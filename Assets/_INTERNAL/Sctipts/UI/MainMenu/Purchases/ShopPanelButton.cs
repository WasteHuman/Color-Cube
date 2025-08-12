using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu.Purchases
{
    public class ShopPanelButton : MonoBehaviour
    {
        [SerializeField] private GameObject _panel;

        private Button _shopButton;

        private void OnEnable()
        {
            _shopButton = _shopButton != null ? _shopButton : GetComponent<Button>();

            _shopButton.onClick.AddListener(ToggleShopPanel);
        }

        private void OnDisable()
        {
            _shopButton.onClick.RemoveListener(ToggleShopPanel);
        }

        private void ToggleShopPanel()
        {
            _panel.SetActive(!_panel.activeSelf);
        }
    }
}