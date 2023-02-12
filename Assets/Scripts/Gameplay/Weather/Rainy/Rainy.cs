using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System;

namespace Game.Gameplay.Weathers
{
    public class Rainy : Weather
    {
        public override WeatherType WeatherType { get => WeatherType.Rainy; }
        [Header("References")]
        public Rain rain;

        public override void Init()
        {
            rain.gameObject.SetActive(false);
        }

        public override async UniTask OnEnterDay()
        {
            // start rainy
            rain.gameObject.SetActive(true);
            rain.StartRaining();
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
            // sun down
            await UniTask.RunOnThreadPool(() => { });
        }

        public override async UniTask OnExitNight()
        {
            // stop rainy
            rain.StopRaining();
            await UniTask.Delay(TimeSpan.FromSeconds(2f));
        }
    }
}