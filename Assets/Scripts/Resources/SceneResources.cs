using UnityEngine;
using Sirenix.OdinInspector;
using WillakeD.ScenePropertyDrawler;

namespace Game
{
    [CreateAssetMenu(menuName = "MyGame/Resources/SceneResources")]
    public class SceneResources : ScriptableObject
    {
        [Title("Scenes")]
        [Required("Must link a scene asset")]
        [Scene]
        public string Menu;
        [SerializeField]
        [Required("Must link a scene asset")]
        [Scene]
        public string Game;
    }
}