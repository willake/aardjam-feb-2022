using UnityEngine;
using Sirenix.OdinInspector;
using WillakeD.ScenePropertyDrawler;

namespace Game
{
    [CreateAssetMenu(menuName = "MyGame/Resources/GameplayResources")]
    public class GameplayResources : ScriptableObject
    {
        [Title("Buildings")]
        public BuildingResources Buildings;
        public VillageResources Village;
        [System.Serializable]
        public class BuildingResources
        {
            public GameObject TopLevel;
            public GameObject MidLevelEmpty;
            public GameObject MidLevelWalls;
        }
        [System.Serializable]
        public class VillageResources
        {
            public GameObject Villager;
        }
    }
}