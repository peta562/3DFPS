using System;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Infrastructure {
    public sealed class SceneLoader {
        readonly ICoroutineRunner _coroutineRunner;

        public SceneName CurrentSceneName { get; private set; }
        
        public SceneLoader(ICoroutineRunner coroutineRunner) {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(SceneName sceneName, Action onLoaded = null) {
            _coroutineRunner.StartCoroutine(LoadScene(sceneName, onLoaded));
        }

        IEnumerator LoadScene(SceneName sceneName, Action onLoaded = null) {
            var nextSceneName = sceneName.ToString();

            if ( SceneManager.GetActiveScene().name == nextSceneName ) {
                onLoaded?.Invoke();
                yield break;
            }

            var nextScene = SceneManager.LoadSceneAsync(nextSceneName);

            while ( !nextScene.isDone ) {
                yield return null;
            }

            CurrentSceneName = sceneName;
            onLoaded?.Invoke();
        }
    }
}