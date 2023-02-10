using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;
using DG.Tweening;
using Game.Gameplay.Environments;

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

        public void Init()
        {
            buildingSystem.Init();
        }

        // prediction outcome phase
        void StartDay()
        {
            //daySystem.SetState(DayState.Day);
            // play weather animation
        }

        // building phase
        void StartMidday()
        {
            //daySystem.SetState(DayState.Midday);
            // play building animation
            // increase building height
        }

        // prediction phase
        void StartNight()
        {
            //daySystem.SetState(DayState.Night);
            // open weather info UI
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