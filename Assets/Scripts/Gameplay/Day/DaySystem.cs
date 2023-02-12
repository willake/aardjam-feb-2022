using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;
using Game.Gameplay.Environments;
using Game.UI;
using Game.Gameplay.Weathers;
using Game.Audios;

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
        public ForecastSystem forecastSystem;

        private GameHUDPanel _gameHUDPanel;

        private bool _willRingBell = true;

        private int currentDay;

        public void Init()
        {
            currentDay = 0;
            buildingSystem.Init();
            gameCamera.LookAt(buildingSystem.GetTowerTopPos());
            gameCamera.Zoom(1.5f);

            _gameHUDPanel = UIManager.instance
                .OpenUI(AvailableUI.GameHUDPanel) as GameHUDPanel;
            _gameHUDPanel.SetDay(currentDay);
            _gameHUDPanel.SetFloor(buildingSystem.Floor);
            _gameHUDPanel.SetTime(DayState.Day);
            _gameHUDPanel.SetVillager(villagerSystem.VillagerAmount);
        }

        // prediction outcome phase
        public async void StartDay()
        {
            if (currentDay != 0)
                weatherSystem.SetWeather(forecastSystem.currentForecastedWeatherType);
            else
                weatherSystem.SetWeather(WeatherType.Sunny);

            if (_willRingBell)
            {
                WrappedAudioClip clip = ResourceManager.instance.AudioResources.gameplayAudios.BellRing;
                AudioManager.instance.PlaySFX(
                    clip.clip,
                    clip.volume
                );
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                AudioManager.instance.PlaySFX(
                    clip.clip,
                    clip.volume
                );
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
                AudioManager.instance.PlaySFX(
                    clip.clip,
                    clip.volume
                );
                await UniTask.Delay(TimeSpan.FromSeconds(0.5f));
            }
            await environmentSystem.ChangeSkyColor(weatherSystem.Weather.dayColor);
            await _gameHUDPanel.SetTimeAsync(DayState.Day);
            await weatherSystem.Weather.OnEnterDay();
            villagerSystem.AppearVillagers();

            if (weatherSystem.Weather.WeatherType == WeatherType.Sunny)
            {
                villagerSystem.AddNewVillager();
                _gameHUDPanel.SetVillager(villagerSystem.VillagerAmount);
            }
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

            await HandleWeatherEffects();

            await weatherSystem.Weather.OnExitMidday();
            StartNight();
        }

        async UniTask HandleWeatherEffects()
        {
            if (_willRingBell)
            {
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
            }

            if (_willRingBell &&
                (weatherSystem.Weather.WeatherType == WeatherType.Thundering
                || weatherSystem.Weather.WeatherType == WeatherType.Rainy))
            {
                await villagerSystem.EliminateVillagers();
                _gameHUDPanel.SetVillager(villagerSystem.VillagerAmount);
            }
        }

        // prediction phase
        async void StartNight()
        {
            Debug.Log("Start Night");
            await weatherSystem.Weather.OnEnterNight();
            await environmentSystem.ChangeSkyColor(weatherSystem.Weather.nightColor);
            await _gameHUDPanel.SetTimeAsync(DayState.Night);

            if (_willRingBell)
            {
                await villagerSystem.MoveVillagersHome();
            }

            ForecastRiddle currentRiddle;

            forecastSystem.SetForecastedWeatherTomorrow();
            currentRiddle = forecastSystem.GenerateRiddle(currentDay == 0);

            // open weather info UI
            PredictionPanel panel =
                await UIManager.instance.OpenUIAsync(AvailableUI.PredictionPanel) as PredictionPanel;
            panel.SetForecast(currentRiddle);
            _willRingBell = await panel.ShowEndDayButton();
            UIManager.instance.Prev();

            currentDay++;
            _gameHUDPanel.SetDay(currentDay);

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