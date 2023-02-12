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
        public EnvironmentSystem environmentSystem;
        public BuildingSystem buildingSystem;
        public VillagerSystem villagerSystem;
        public WeatherSystem weatherSystem;
        public ForecastSystem forecastSystem;

        private const float dayAnimLength = 2f;
        private const float middayVillagerAnimLength = 2f;
        private const float middayBuildingAnimLength = 3f;
        private const float nightAnimLength = 3f;

        private int currentDay;

        public void Init()
        {
            currentDay = 0;
            buildingSystem.Init();
        }

        // prediction outcome phase
        public async void StartDay()
        {
            if (currentDay != 0)
            {
                Debug.Log(forecastSystem.currentForecastedWeatherType);
                weatherSystem.SetWeather(forecastSystem.currentForecastedWeatherType);
            }
            else
            {
                weatherSystem.SetWeather(WeatherType.Sunny);
            }

            await environmentSystem.ChangeSkyColor(weatherSystem.Weather.dayColor);
            await weatherSystem.Weather.OnEnterDay();
            villagerSystem.AppearVillagers();
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
            await weatherSystem.Weather.OnEnterMidday();
            Debug.Log($"Increase 1 villager. Now is {villagerSystem.VillagerAmount}");
            //Move villagers
            await villagerSystem.MoveVillagersToTower();
            await villagerSystem.StartTowerWork();
            buildingSystem.IncreaseFloor();
            Debug.Log($"Increase 1 floor. Now is {buildingSystem.Height}");

            await weatherSystem.Weather.OnExitMidday();
            StartNight();
        }

        // prediction phase
        async void StartNight()
        {
            Debug.Log("Start Night");
            await weatherSystem.Weather.OnEnterNight();
            await environmentSystem.ChangeSkyColor(weatherSystem.Weather.nightColor);

            await villagerSystem.MoveVillagersHome();

            forecastSystem.SetForecastedWeatherTomorrow();

            // open weather info UI
            PredictionPanel panel =
                await UIManager.instance.OpenUIAsync(AvailableUI.PredictionPanel) as PredictionPanel;
            await panel.ShowEndDayButton();
            UIManager.instance.Prev();

            currentDay++;

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