using UnityEngine;
using UniRx;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using WillakeD.ScenePropertyDrawler;
using System;

namespace Game
{
    public class SceneLoader : MonoBehaviour
    {
        private string _currentScene = string.Empty;

        private Subject<string> _loaderWillLoadSceneSubject = new Subject<string>();
        public IObservable<string> LoaderWillLoadScene
        {
            get => _loaderWillLoadSceneSubject.AsObservable();
        }
        private Subject<string> _loaderDidLoadSceneSubject = new Subject<string>();
        public IObservable<string> LoaderDidLoadScene
        {
            get => _loaderDidLoadSceneSubject.AsObservable();
        }

        public async UniTask SwitchScene(AvailableScene scene)
        {
            string sceneName = GetSceneName(scene);

            _loaderWillLoadSceneSubject.OnNext(sceneName);

            await LoadScene(sceneName);

            _loaderDidLoadSceneSubject.OnNext(sceneName);
        }

        public string GetSceneName(AvailableScene scene)
        {
            switch (scene)
            {
                case AvailableScene.Menu:
                default:
                    return ResourceManager.instance.SceneResources.Menu.GetSceneNameByPath();
                case AvailableScene.MainGame:
                    return ResourceManager.instance.SceneResources.Game.GetSceneNameByPath();
            }
        }

        private async UniTask LoadScene(string sceneName)
        {
            AsyncOperation loadSceneOperation =
                SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            await loadSceneOperation;

            loadSceneOperation.allowSceneActivation = false;

            if (_currentScene != string.Empty)
            {
                AsyncOperation unloadSceneOperation =
                    SceneManager.UnloadSceneAsync(_currentScene);

                await unloadSceneOperation;
            }

            loadSceneOperation.allowSceneActivation = true;

            _currentScene = sceneName;
        }
    }
}