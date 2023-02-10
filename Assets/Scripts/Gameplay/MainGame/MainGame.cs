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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A)) daySystem.SetState(DayState.Day);
            if (Input.GetKeyDown(KeyCode.S)) daySystem.SetState(DayState.Midday);
            if (Input.GetKeyDown(KeyCode.D)) daySystem.SetState(DayState.Night);
            if (Input.GetKeyDown(KeyCode.Q)) buildingSystem.IncreaseFloor();
        }
    }
}
