using UnityEngine;
using DG.Tweening;

namespace Game.Gameplay.Weathers
{
    public class Moon : MonoBehaviour
    {
        [Header("Settings")]
        public float riseHieght = 3.78f;
        public float downHieght = -5;
        public float duration = 0.5f;
        public Ease ease = Ease.InOutSine;

        public void SetHieght(State state)
        {
            Vector3 pos = transform.position;
            if (state == State.Rise)
            {
                pos.y = riseHieght;
            }
            else
            {
                pos.y = downHieght;
            }
            transform.position = pos;
        }
        public Tween Rise()
        {
            return transform.DOMoveY(riseHieght, duration).SetEase(ease);
        }

        public Tween Down()
        {
            return transform.DOMoveY(downHieght, duration).SetEase(ease);
        }

        public enum State
        {
            Rise,
            Down
        }
    }
}