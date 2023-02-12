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
        public List<BuildingBlock> floors { get { return _floors; } }

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
            int r = Random.Range(0, 2);

            if (r > 0)
            {
                _floors.Add(
                    factory.GenerateBuildingBlock(
                        BuildingBlockType.MidLevelEmpty,
                        buildingBase));
            }
            else
            {
                _floors.Add(
                    factory.GenerateBuildingBlock(
                        BuildingBlockType.MidLevelWalls,
                        buildingBase));
            }
            UpdateFloors();
        }

        private void UpdateFloors()
        {
            float height = 0;
            for (int i = _floors.Count - 1; i >= 0; i--)
            {
                _floors[i].transform.position =
                    buildingBase.position + new Vector3(0, height, i * 0.001f);
                height += GetBuildingHeight(_floors[i].Type);
            }
        }

        private float GetBuildingHeight(BuildingBlockType type)
        {
            switch (type)
            {
                case BuildingBlockType.TopLevel: return 1.1875f;
                case BuildingBlockType.MidLevelEmpty: return 0.6875f;
                case BuildingBlockType.MidLevelWalls: return 0.7283f;
                default: return 1;
            }
        }
    }
}