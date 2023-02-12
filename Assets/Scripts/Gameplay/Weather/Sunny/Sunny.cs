using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Game.Gameplay.Weathers
{
    public class Sunny : Weather
    {
        public override WeatherType WeatherType { get => WeatherType.Sunny; }
        [Header("References")]
        public Transform[] clouds;
        public Sun sun;
        public Moon moon;
        public GameObject staticStars;
        public GameObject dynamicStars;

        [Header("Settings")]
        public float cloudMoveDuration = 2f;
        public Ease cloudMoveEase = Ease.OutSine;

        public override void Init()
        {
            foreach (Transform cloud in clouds)
            {
                cloud.gameObject.SetActive(false);
            }
            sun.SetHieght(Sun.State.Down);
            moon.SetHieght(Moon.State.Down);
            staticStars.gameObject.SetActive(false);
            dynamicStars.gameObject.SetActive(false);
        }

        public override async UniTask OnEnterDay()
        {
            // sun on;
            await sun.Rise().AsyncWaitForCompletion();
            staticStars.gameObject.SetActive(false);
            dynamicStars.gameObject.SetActive(false);
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
            await sun.Down().AsyncWaitForCompletion();
        }

        public override async UniTask OnEnterNight()
        {
            staticStars.gameObject.SetActive(true);
            dynamicStars.gameObject.SetActive(true);
            await moon.Rise().AsyncWaitForCompletion();
            // sun down
        }

        public override async UniTask OnExitNight()
        {
            // nothing
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
            await moon.Down().AsyncWaitForCompletion();
        }
    }
}
