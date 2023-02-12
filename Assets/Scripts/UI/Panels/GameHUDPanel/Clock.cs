using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Gameplay;
using DG.Tweening;

namespace Game.UI
{
    public class Clock : MonoBehaviour
    {
        [Header("References")]
        public Transform indicator;

        [Header("Settings")]
        public float rotateDuration = 0.4f;
        public Ease rotateEase = Ease.OutBounce;

        public void SetTime(DayState dayState)
        {
            indicator.transform.rotation = GetIndicatorQuaternion(dayState);
        }

        public Tween SetTimeAsync(DayState dayState)
        {
            return indicator.transform.DORotateQuaternion(
                GetIndicatorQuaternion(dayState),
                rotateDuration
            ).SetEase(rotateEase);
        }

        private Quaternion GetIndicatorQuaternion(DayState dayState)
        {
            switch (dayState)
            {
                case DayState.Day:
                default:
                    return Quaternion.Euler(0, 0, 55);
                case DayState.Midday:
                    return Quaternion.Euler(0, 0, 0);
                case DayState.Night:
                    return Quaternion.Euler(0, 0, -55);
            }
        }
    }
}