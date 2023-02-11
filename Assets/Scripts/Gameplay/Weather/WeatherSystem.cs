using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public class WeatherSystem : MonoBehaviour
    {
        [Header("References")]
        public Sunny sunny;
        public Weather Weather { get; private set; }
        public void SetWeather(WeatherType weatherType)
        {
            switch (weatherType)
            {
                case WeatherType.Sunny:
                default:
                    Weather = sunny;
                    Weather.gameObject.SetActive(true);
                    break;
            }

            Weather.Init();
        }
    }

    public enum WeatherType
    {
        Sunny,
        Rainy,
        Windy,
        Cloudy,
        Foggy
    }
}