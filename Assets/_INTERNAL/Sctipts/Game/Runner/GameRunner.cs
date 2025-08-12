using Game.SoundsSystem;
using Gameplay.SaveAndLoadServices;
using Other;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

namespace Core.Runner
{
    public class GameRunner : MonoBehaviour
    {
        [Header("Loading screen settings")]
        [SerializeField] private GameScene _loadableScene;
        [SerializeField] private Slider _progressBar;

        [Space(5), Header("Game systems")]
        [SerializeField] private AudioSystem _audioSystem;
#if !UNITY_ANDROID
        [SerializeField] private SaveSystemMono _saveSystemMono;
#endif
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
        }

        private void CheckLoadingState()
        {
            float progress = Mathf.Clamp01(_loadingOperation.progress / 0.9f);

            _progressBar.value = progress;

            Debug.Log($"Loading progress: {progress * 100}%");

            if (_loadingOperation.progress >= 0.9f)
            {
                _loadingOperation.allowSceneActivation = true;
#if !UNITY_ANDROID
                _saveSystemMono.LoadData();
                YG2.GameReadyAPI();
#endif
            }
        }
    }
}