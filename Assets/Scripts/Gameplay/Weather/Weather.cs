using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public abstract class Weather : MonoBehaviour
    {
        public abstract WeatherType WeatherType { get; }
        public abstract void Init();
        public abstract UniTask OnEnterDay();
        public abstract UniTask OnExitDay();
        public abstract UniTask OnEnterMidday();
        public abstract UniTask OnExitMidday();
        public abstract UniTask OnEnterNight();
        public abstract UniTask OnExitNight();
    }
}