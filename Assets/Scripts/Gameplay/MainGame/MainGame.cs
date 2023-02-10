using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI;

namespace Game.Gameplay
{
    public class MainGame : GameScene
    {
        [Header("References")]
        public DaySystem daySystem;
        public BuildingSystem buildingSystem;
        void Start()
        {
            UIManager.instance.OpenUI(AvailableUI.GameHUDPanel);
            buildingSystem.Init();
        }

        void PlayIntro()
        {

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
