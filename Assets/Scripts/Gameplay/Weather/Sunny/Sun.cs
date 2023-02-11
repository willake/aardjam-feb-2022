using UnityEngine;
using DG.Tweening;

namespace Game.Gameplay.Weathers
{
    public class Sun : MonoBehaviour
    {
        [Header("Settings")]
        public float riseHieght = 3.78f;
        public float dawnHieght = -5;
        public float duration = 0.5f;
        public Ease ease = Ease.InOutSine;

        public void SetHieght(State state)
        {
            Vector2 pos = transform.position;
            if (state == State.Rise)
            {
                pos.y = riseHieght;
            }
            else
            {
                pos.y = dawnHieght;
            }
        }
        public Tween Rise()
        {
            return transform.DOMoveY(riseHieght, duration).SetEase(ease);
        }

        public Tween Dawn()
        {
            return transform.DOMoveY(dawnHieght, duration).SetEase(ease);
        }

        public enum State
        {
            Rise,
            Dawn
        }
    }
}