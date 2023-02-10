using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;
using System;
using Cysharp.Threading.Tasks;

namespace Game.Gameplay
{
    public class MainGame : GameScene
    {
        [Header("References")]
        public DaySystem daySystem;
        async void Start()
        {
            UIManager.instance.OpenUI(AvailableUI.GameHUDPanel);
            daySystem.Init();
            await PlayIntro();
            daySystem.StartDay();
        }

        async UniTask PlayIntro()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));
        }
    }
}
