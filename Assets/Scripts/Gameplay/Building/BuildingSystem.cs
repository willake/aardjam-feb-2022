using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class BuildingSystem : MonoBehaviour
    {
        [Header("References")]
        public BuildingBlockFactory factory;
        public Transform buildingBase;

        private List<BuildingBlock> _floors = new List<BuildingBlock>();
        public int Height { get; private set; }

        public void Init()
        {
            Height = 0;
            _floors.Add(
                factory.GenerateBuildingBlock(BuildingBlockType.TopLevel, buildingBase)
            );
            UpdateFloors();
        }

        public void IncreaseFloor()
        {
            Height += 1;
            _floors.Add(
                factory.GenerateBuildingBlock(BuildingBlockType.MidLevelEmpty, buildingBase)
            );
            UpdateFloors();
        }

        private void UpdateFloors()
        {
            for (int i = _floors.Count - 1; i >= 0; i--)
            {
                _floors[i].transform.position =
                    buildingBase.position + CalculateBuildingHeight(
                        _floors.Count - 1 - i);
            }
        }

        private Vector3 CalculateBuildingHeight(int floor)
        {
            return new Vector3(0, floor * 1f, 0);
        }
    }
}