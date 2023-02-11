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
        // will add it back later
        //public VillagerType Type { get; private set; }

        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _renderer;

        private Tween _bounceTween;

        public void Init(int id, float speed)
        {
            ID = id;
            Speed = speed;
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