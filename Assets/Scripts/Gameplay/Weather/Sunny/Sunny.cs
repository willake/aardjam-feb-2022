using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Game.Gameplay.Weathers
{
    public class Sunny : Weather
    {
        public override WeatherType WeatherType { get => WeatherType.Sunny; }
        [Header("References")]
        public Sun sun;
        public Moon moon;
        public GameObject stars;

        public override void Init()
        {
            sun.SetHieght(Sun.State.Dawn);
            moon.SetHieght(Moon.State.Down);
            stars.gameObject.SetActive(false);
        }

        public override async UniTask OnEnterDay()
        {
            // sun on;
            await sun.Rise().AsyncWaitForCompletion();
        }

        public override async UniTask OnExitDay()
        {
            // nothing
            await UniTask.RunOnThreadPool(() => { });
        }

        public override async UniTask OnEnterMidday()
        {
            // nothing
            await UniTask.RunOnThreadPool(() => { });
        }

        public override async UniTask OnExitMidday()
        {
            // nothing
            await sun.Dawn().AsyncWaitForCompletion();
        }

        public override async UniTask OnEnterNight()
        {
            stars.gameObject.SetActive(true);
            await moon.Rise().AsyncWaitForCompletion();
            // sun down
        }

        public override async UniTask OnExitNight()
        {
            // nothing
            await moon.Down().AsyncWaitForCompletion();
            stars.gameObject.SetActive(false);
        }
    }
}
