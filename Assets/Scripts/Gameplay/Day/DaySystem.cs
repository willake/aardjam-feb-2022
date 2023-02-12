using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;
using Game.Gameplay.Environments;
using Game.UI;
using Game.Gameplay.Weathers;

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
        public GameCamera gameCamera;
        public EnvironmentSystem environmentSystem;
        public BuildingSystem buildingSystem;
        public VillagerSystem villagerSystem;
        public WeatherSystem weatherSystem;

        private GameHUDPanel _gameHUDPanel;

        public void Init()
        {
            buildingSystem.Init();
            gameCamera.LookAt(buildingSystem.GetTowerTopPos());
            gameCamera.Zoom(1.5f);

            _gameHUDPanel = UIManager.instance
                .OpenUI(AvailableUI.GameHUDPanel) as GameHUDPanel;
            _gameHUDPanel.SetTime(DayState.Day);
        }

        // prediction outcome phase
        public async void StartDay()
        {
            Debug.Log("Start Day");
            Debug.Log("Today is sunny");
            weatherSystem.SetWeather(WeatherType.Rainy);
            await environmentSystem.ChangeSkyColor(weatherSystem.Weather.dayColor);
            await _gameHUDPanel.SetTimeAsync(DayState.Day);
            await weatherSystem.Weather.OnEnterDay();
            villagerSystem.AddNewVillager();
            // play weather animation
            await weatherSystem.Weather.OnExitDay();
            StartMidday();
        }

        // building phase
        async void StartMidday()
        {
            Debug.Log("Start Midday");
            await environmentSystem.ChangeSkyColor(weatherSystem.Weather.middayColor);
            await _gameHUDPanel.SetTimeAsync(DayState.Midday);
            await weatherSystem.Weather.OnEnterMidday();
            Debug.Log($"Increase 1 villager. Now is {villagerSystem.VillagerAmount}");
            //Move villagers
            await villagerSystem.MoveVillagersToTower();
            await villagerSystem.StartTowerWork();
            buildingSystem.IncreaseFloor();
            Debug.Log($"Increase 1 floor. Now is {buildingSystem.Floor}");

            Sequence sequence = DOTween.Sequence();

            sequence
                .Append(gameCamera.LookAtAsync(buildingSystem.GetTowerTopPos()))
                .Join(gameCamera.ZoomAsync(buildingSystem.Height));

            await weatherSystem.Weather.OnExitMidday();
            StartNight();
        }

        // prediction phase
        async void StartNight()
        {
            Debug.Log("Start Night");
            await weatherSystem.Weather.OnEnterNight();
            await environmentSystem.ChangeSkyColor(weatherSystem.Weather.nightColor);
            await _gameHUDPanel.SetTimeAsync(DayState.Night);

            await villagerSystem.MoveVillagersHome();

            // open weather info UI
            PredictionPanel panel =
                await UIManager.instance.OpenUIAsync(AvailableUI.PredictionPanel) as PredictionPanel;
            await panel.ShowEndDayButton();
            UIManager.instance.Prev();

            await weatherSystem.Weather.OnExitNight();
            StartDay();
            // wait for end day
        }

        private void Update()
        {
            //if (Input.GetKeyDown(KeyCode.A)) daySystem.SetState(DayState.Day);
            //if (Input.GetKeyDown(KeyCode.S)) daySystem.SetState(DayState.Midday);
            //if (Input.GetKeyDown(KeyCode.D)) daySystem.SetState(DayState.Night);
            if (Input.GetKeyDown(KeyCode.Q)) buildingSystem.IncreaseFloor();
        }
    }
}