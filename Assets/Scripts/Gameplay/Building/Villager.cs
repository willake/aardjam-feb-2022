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

        private Animator _animator;
        private SpriteRenderer _renderer;

        private Animator GetAnimator()
        {
            if (_animator == null) _animator = GetComponent<Animator>();

            return _animator;
        }

        private SpriteRenderer GetRenderer()
        {
            if (_renderer == null) _renderer = GetComponent<SpriteRenderer>();

            return _renderer;
        }

        private Tween _bounceTween;

        public void Init(int id, float speed)
        {
            ID = id;
            Speed = speed;
        }

        public async UniTask MoveTo(Vector3 target)
        {
            float distance = Mathf.Abs(target.x - transform.position.x);

            GetRenderer().flipX = target.x - transform.position.x > 0;

            float baseHeight = transform.position.y;

            GetAnimator().SetBool("isMoving", true);

            // // bounce
            // _bounceTween = DOTween.Sequence()
            //         .Append(transform.DOMoveY(baseHeight + 0.1f, 0.1f))
            //         .Append(transform.DOMoveY(baseHeight + 0.0f, 0.05f))
            //         .SetLoops(-1);

            await transform
                .DOMoveX(target.x, distance / Speed).SetEase(Ease.Linear).AsyncWaitForCompletion();

            _bounceTween?.Kill();

            GetAnimator().SetBool("isMoving", false);
            Vector2 pos = transform.position;
            pos.y = baseHeight;
            transform.position = pos;
        }

        public async UniTask AnimateWork()
        {
            float baseHeight = transform.position.y;

            await DOTween.Sequence()
                            .Append(transform.DOMoveY(baseHeight + Random.Range(0.5f, 1.5f), Random.Range(0.1f, 0.3f)))
                            .Append(transform.DOMoveY(baseHeight + 0.0f, 0.1f))
                            .SetLoops(4)
                            .AsyncWaitForCompletion();

            Vector2 pos = transform.position;
            pos.y = baseHeight;
            transform.position = pos;
        }

        public enum VillagerType
        {
            Normal,
            Lazy,
            Fast
        }
    }
}