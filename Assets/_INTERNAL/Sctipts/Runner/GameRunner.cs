using Gameplay.SaveAndLoadServices;
using Gameplay.SoundsSystem;
using Other;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Runner
{
    public class GameRunner : MonoBehaviour
    {
        [Header("Loading screen settings")]
        [SerializeField] private GameScene _loadableScene;
        [SerializeField] private GameObject _loadingScreen;
        [SerializeField] private Slider _progressBar;

        [Space(5), Header("Game systems")]
        [SerializeField] private AudioSystem _audioSystem;
        [SerializeField] private SaveSystemMono _saveSystemMono;

        private AsyncOperation _loadingOperation;

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            InitializeSystems();

            _loadingOperation = SceneManager.LoadSceneAsync(_loadableScene.SceneName);
            _loadingOperation.allowSceneActivation = false;

            while (!_loadingOperation.isDone)
            {
                CheckLoadingState();
                yield return null;
            }
        }

        private void InitializeSystems()
        {
            _audioSystem.Initialize();
            _saveSystemMono.LoadData();
        }

        private void CheckLoadingState()
        {
            float progress = Mathf.Clamp01(_loadingOperation.progress / 0.9f);

            _progressBar.value = progress;

            Debug.Log($"Loading progress: {progress * 100}%");

            if (_loadingOperation.progress >= 0.9f)
            {
                _loadingOperation.allowSceneActivation = true;
            }
        }
    }
}