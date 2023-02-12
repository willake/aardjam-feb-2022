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
        public WeatherResources Weathers;
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
        [System.Serializable]
        public class WeatherResources
        {
            public GameObject Sunny;
            public GameObject Rainy;
            public GameObject ForecastRiddle;
        }
    }
}