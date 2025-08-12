using Game.SoundsSystem;
using Other;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameScene _gameplayScene;
        [SerializeField] private Button _startButton;

        private void OnEnable()
        {
            _startButton.onClick.AddListener(StartGame);
        }

        private void OnDisable()
        {
            _startButton.onClick.RemoveListener(StartGame);
        }

        private void StartGame()
        {
            AudioSystem.Instance.PlaySoundByID(SoundID.Click);
            SceneManager.LoadSceneAsync(_gameplayScene.SceneName, LoadSceneMode.Single);
        }
    }
}