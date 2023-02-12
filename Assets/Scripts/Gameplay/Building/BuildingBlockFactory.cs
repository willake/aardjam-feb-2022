using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay
{
    public enum BuildingBlockType
    {
        TopLevel,
        MidLevelEmpty,
        MidLevelWalls
    }

    public class BuildingBlockFactory : MonoBehaviour
    {
        public BuildingBlock GenerateBuildingBlock(BuildingBlockType type, Transform parent)
        {
            switch (type)
            {
                case BuildingBlockType.TopLevel:
                default:
                    GameObject topGo = Instantiate(
                        ResourceManager.instance.GameplayResources.Buildings.TopLevel,
                        Vector3.zero,
                        Quaternion.identity, parent);
                    return topGo.GetComponent<BuildingBlock>();
                case BuildingBlockType.MidLevelEmpty:
                    GameObject emptyGo = Instantiate(
                        ResourceManager.instance.GameplayResources.Buildings.MidLevelEmpty,
                        Vector3.zero,
                        Quaternion.identity, parent);
                    return emptyGo.GetComponent<BuildingBlock>();
                case BuildingBlockType.MidLevelWalls:
                    GameObject wallsGo = Instantiate(
                        ResourceManager.instance.GameplayResources.Buildings.MidLevelWalls,
                        Vector3.zero,
                        Quaternion.identity, parent);
                    return wallsGo.GetComponent<BuildingBlock>();
            }
        }
    }
}