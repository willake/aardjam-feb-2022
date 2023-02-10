using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;

namespace Game.Gameplay
{
    public enum DayState
    {
        Day,
        Midday,
        Night
    }
    public class DaySystem : MonoBehaviour
    {
        [Header("References")]
        public Light2D globalLight;

        [Header("Settings")]
        public Color dayColor;
        public Color middayColor;
        public Color nightColor;
        public float transitionDuration = 0.4f;
        public Ease transitionEase = Ease.Linear;

        public DayState State { get; private set; }

        public async void SetState(DayState state)
        {
            await OnExitState(State);
            State = state;
            await OnEnterState(state);
        }

        private async UniTask OnEnterState(DayState state)
        {
            Color target = GetDayStateColor(state);
            await DOTween.To(
                () => globalLight.color,
                (color) => globalLight.color = color,
                target,
                transitionDuration
            ).SetEase(transitionEase).AsyncWaitForCompletion();
        }

        private async UniTask OnExitState(DayState state)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(1));
        }

        private Color GetDayStateColor(DayState state)
        {
            switch (state)
            {
                case DayState.Day:
                default:
                    return dayColor;
                case DayState.Midday:
                    return middayColor;
                case DayState.Night:
                    return nightColor;
            }
        }
    }
}