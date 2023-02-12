using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public class ForecastSystem : MonoBehaviour
    {
        [Header("References")]
        public WeatherFactory weatherFactory;

        [SerializeField] private List<DropTable<WeatherType>> weatherProbabilities;

        public WeatherType currentForecastedWeatherType { get; private set; }

        public WeatherType SetForecastedWeatherTomorrow()
        {
            currentForecastedWeatherType = weatherProbabilities[0].ReturnLootFromTable<WeatherType>(weatherProbabilities);
            return currentForecastedWeatherType;
        }

    }
}