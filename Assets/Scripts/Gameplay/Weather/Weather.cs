using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public abstract class Weather : MonoBehaviour
    {
        public abstract WeatherType WeatherType { get; }
        public abstract UniTask OnEnterDay();
        public abstract UniTask OnEnterMidday();
        public abstract UniTask OnEnterNight();
    }
}