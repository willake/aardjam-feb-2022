using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using System;
using DG.Tweening;

namespace Game.Gameplay.Weathers
{
    public class Rainy : Weather
    {
        public override WeatherType WeatherType { get => WeatherType.Rainy; }
        [Header("References")]
        public Transform[] clouds;
        public Rain rain;

        [Header("Settings")]
        public float cloudMoveDuration = 2f;
        public Ease cloudMoveEase = Ease.OutSine;

        public override void Init()
        {
            foreach (Transform cloud in clouds)
            {
                cloud.gameObject.SetActive(false);
            }
            rain.gameObject.SetActive(false);
        }

        public override async UniTask OnEnterDay()
        {
            // start rainy
            rain.gameObject.SetActive(true);

            Sequence sequence = DOTween.Sequence();

            foreach (Transform cloud in clouds)
            {
                cloud.gameObject.SetActive(true);
                float x = cloud.position.x;

                if (x > 0)
                {
                    cloud.position = cloud.position
                        + new Vector3(10, 0, 0);
                }
                else
                {
                    cloud.position = cloud.position
                        + new Vector3(-10, 0, 0);
                }

                sequence.Join(
                    cloud.DOMoveX(x, cloudMoveDuration).SetEase(cloudMoveEase)
                );
            }

            await sequence.AsyncWaitForCompletion();
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

            Sequence sequence = DOTween.Sequence();

            foreach (Transform cloud in clouds)
            {
                cloud.gameObject.SetActive(true);
                float x = cloud.position.x;

                if (x > 0)
                {
                    sequence.Join(
                        cloud.DOMoveX(x + 10, cloudMoveDuration).SetEase(cloudMoveEase)
                    );
                }
                else
                {

                    sequence.Join(
                        cloud.DOMoveX(x - 10, cloudMoveDuration).SetEase(cloudMoveEase)
                    );
                }
            }

            await sequence.AsyncWaitForCompletion();
        }
    }
}