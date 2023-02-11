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

        public override async UniTask OnEnterDay()
        {
            // sun on;
            sun.SetHieght(Sun.State.Dawn);
            await sun.Rise().AsyncWaitForCompletion();
        }

        public override async UniTask OnEnterMidday()
        {
            // nothing
        }

        public override async UniTask OnEnterNight()
        {
            await sun.Dawn().AsyncWaitForCompletion();
            // sun down
        }
    }
}
