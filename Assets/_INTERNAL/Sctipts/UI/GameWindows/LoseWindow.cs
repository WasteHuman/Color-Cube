using UI;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameWindows
{
    public class LoseWindow : SimpleWindow
    {
        [Space(10), Header("In-window buttons")]
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _someButton;

        [Space(10), Header("Main menu scene")]
        [SerializeField] private SceneAsset _mainMenuScene;

        private void OnEnable()
        {
            _resetButton.onClick.AddListener(ResetGame);
        }

        private void OnDisable()
        {
            _resetButton.onClick.RemoveListener(ResetGame);
        }

        private void ResetGame()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }

        protected override void OnClosed()
        {
            SceneManager.LoadSceneAsync(_mainMenuScene.name);
        }
    }
}