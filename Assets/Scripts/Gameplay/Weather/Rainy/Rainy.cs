using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public class Rainy : Weather
    {
        public override WeatherType WeatherType { get => WeatherType.Rainy; }
        public override void Init()
        {
        }

        public override async UniTask OnEnterDay()
        {
            // sun on;
            await UniTask.RunOnThreadPool(() => { });
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
            await UniTask.RunOnThreadPool(() => { });
        }

        public override async UniTask OnEnterNight()
        {
            await UniTask.RunOnThreadPool(() => { });
            // sun down
        }

        public override async UniTask OnExitNight()
        {
            // nothing
            await UniTask.RunOnThreadPool(() => { });
        }
    }
}