using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay
{
    public class Villager : MonoBehaviour
    {
        public int ID { get; private set; }
        public float Speed { get; private set; }
        public string Name { get; private set; }
        // will add it back later
        //public VillagerType Type { get; private set; }

        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _renderer;

        private Tween _bounceTween;

        public void Init(int id, float speed, string name)
        {
            ID = id;
            Speed = speed;
            Name = name;
        }

        public async UniTask MoveTo(Vector3 target, bool disappearOnTargetReached = false)
        {
            float distance = Mathf.Abs(target.x - transform.position.x);

            _renderer.flipX = target.x - transform.position.x > 0;

            float baseHeight = transform.position.y;

            _animator.SetBool("isMoving", true);

            await transform
                .DOMoveX(target.x, distance / Speed).SetEase(Ease.Linear).AsyncWaitForCompletion();

            _bounceTween?.Kill();

            _animator.SetBool("isMoving", false);
            if (disappearOnTargetReached)
                AnimateVisibleToggle(false);

            Vector2 pos = transform.position;
            pos.y = baseHeight;
            transform.position = pos;
        }

        public async UniTask AnimateWork()
        {
            float baseHeight = transform.position.y;

            await DOTween.Sequence()
                            .Append(transform.DOMoveY(baseHeight + Random.Range(0.5f, 1f), Random.Range(0.1f, 0.3f)))
                            .Append(transform.DOMoveY(baseHeight + 0.0f, 0.15f))
                            .SetLoops(4)
                            .AsyncWaitForCompletion();

            Vector2 pos = transform.position;
            pos.y = baseHeight;
            transform.position = pos;
        }

        public async UniTask AnimateElimination()
        {
            var firstTarget = new Vector2(Random.Range(transform.position.x + 2, transform.position.x - 2), Random.Range(transform.position.y + 2, transform.position.y + 3));
            float distanceY = Mathf.Abs(firstTarget.y - transform.position.y);

            await transform
                .DOMove(firstTarget, distanceY / 10).SetEase(Ease.Linear).AsyncWaitForCompletion();

            var secondTarget = new Vector2(Random.Range(transform.position.x + 2, transform.position.x - 2), -5);
            distanceY = Mathf.Abs(secondTarget.y - transform.position.y);

            await transform
                .DOMove(secondTarget, distanceY / 15).SetEase(Ease.Linear).AsyncWaitForCompletion();
        }

        public void AnimateVisibleToggle(bool toggle = true)
        {
            _animator.SetBool("isVisible", toggle);
        }

        public enum VillagerType
        {
            Normal,
            Lazy,
            Fast
        }
    }
}