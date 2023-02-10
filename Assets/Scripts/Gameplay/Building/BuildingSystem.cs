using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public class BuildingSystem : MonoBehaviour
    {
        [Header("References")]
        public Transform buildingBase;

        [Header("Settings")]
        public GameObject bellPrefab;
        public GameObject normalPrefab;

        private List<BuildingBlock> _floors = new List<BuildingBlock>();
        public int Height { get; private set; }

        public void Init()
        {
            Height = 0;
            _floors.Add(
                GenerateBuildingBlock(BuildingBlockType.Bell)
            );
            UpdateFloors();
        }

        public void IncreaseFloor()
        {
            Height += 1;
            _floors.Add(
                GenerateBuildingBlock(BuildingBlockType.Normal)
            );
            UpdateFloors();
        }

        private BuildingBlock GenerateBuildingBlock(BuildingBlockType type)
        {
            switch (type)
            {
                case BuildingBlockType.Bell:
                default:
                    GameObject bellGo = Instantiate(
                        bellPrefab,
                        Vector3.zero,
                        Quaternion.identity, buildingBase);
                    return bellGo.GetComponent<BuildingBlock>();
                case BuildingBlockType.Normal:
                    GameObject normalGo = Instantiate(
                        normalPrefab,
                        Vector3.zero,
                        Quaternion.identity, buildingBase);
                    return normalGo.GetComponent<BuildingBlock>();
            }
        }

        private void UpdateFloors()
        {
            for (int i = _floors.Count - 1; i >= 0; i--)
            {
                Debug.Log($"Floor index {i} will be placed at {_floors.Count - 1 - i} floor");
                _floors[i].transform.position =
                    buildingBase.position + CalculateBuildingHeight(
                        _floors.Count - 1 - i);
            }
        }

        private Vector3 CalculateBuildingHeight(int floor)
        {
            return new Vector3(0, floor * 1f, 0);
        }

        public enum BuildingBlockType
        {
            Bell,
            Normal
        }
    }
}