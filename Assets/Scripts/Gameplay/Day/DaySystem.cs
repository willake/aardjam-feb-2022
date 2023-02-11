using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;
using Game.Gameplay.Environments;
using Game.UI;

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

        private const float dayAnimLength = 2f;
        private const float middayVillagerAnimLength = 2f;
        private const float middayBuildingAnimLength = 3f;
        private const float nightAnimLength = 3f;

        public void Init()
        {
            buildingSystem.Init();
            villagerSystem.Init();
            villagerSystem.onAllVillagersReachedTower += EndMidday;
        }

        // prediction outcome phase
        public async void StartDay()
        {
            Debug.Log("Start Day");
            await environmentSystem.SetState(DayState.Day);
            Debug.Log("Today is sunny");
            await UniTask.Delay(TimeSpan.FromSeconds(dayAnimLength));
            villagerSystem.AddNewVillager();
            // play weather animation
            StartMidday();
        }

        // building phase
        async void StartMidday()
        {
            Debug.Log("Start Midday");
            await environmentSystem.SetState(DayState.Midday);
            Debug.Log($"Increase 1 villager. Now is {villagerSystem.villagerAmount}");
            await UniTask.Delay(TimeSpan.FromSeconds(middayVillagerAnimLength));
            //Move villagers
            villagerSystem.MoveVillagersToTower();
        }

        async void EndMidday()
        {
            // play building animation
            villagerSystem.StartTowerWork();
            await UniTask.Delay(TimeSpan.FromSeconds(middayBuildingAnimLength));
            // increase building height
            buildingSystem.IncreaseFloor();
            Debug.Log($"Increase 1 floor. Now is {buildingSystem.Height}");
            StartNight();
        }

        // prediction phase
        async void StartNight()
        {
            Debug.Log("Start Night");
            await environmentSystem.SetState(DayState.Night);

            villagerSystem.MoveVillagersHome();

            // open weather info UI
            PredictionPanel panel =
                await UIManager.instance.OpenUIAsync(AvailableUI.PredictionPanel) as PredictionPanel;
            await panel.ShowEndDayButton();
            UIManager.instance.Prev();
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