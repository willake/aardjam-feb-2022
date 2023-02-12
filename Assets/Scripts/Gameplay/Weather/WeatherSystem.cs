using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public class WeatherSystem : MonoBehaviour
    {
        [Header("References")]
        public WeatherFactory weatherFactory;
        public Weather Weather { get; private set; }

        public void SetWeather(WeatherType weatherType)
        {
            if (Weather != null)
            {
                Destroy(Weather.gameObject);
            }

            Weather = weatherFactory.GenerateWeather(weatherType, transform);

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