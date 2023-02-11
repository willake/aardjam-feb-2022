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
        public class BuildingResources
        {
            public GameObject TopLevel;
            public GameObject MidLevelEmpty;
            public GameObject MidLevelWalls;
        }
    }
}