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
            daySystem.Init();
            await PlayIntro();
            daySystem.StartDay();
        }

        async UniTask PlayIntro()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.instance.PauseGame();
                OpenPausePanel();
            }
        }

        private void OpenPausePanel()
        {
            UIManager.instance.OpenUIAsync(AvailableUI.PausePanel).Forget();
        }
    }
}
