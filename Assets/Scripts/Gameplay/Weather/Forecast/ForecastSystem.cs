using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public class ForecastSystem : MonoBehaviour
    {
        [Header("References")]
        public VillagerSystem villagerSystem;

        [SerializeField] private List<DropTable<WeatherType>> weatherProbabilities;

        private int villagerLimit = 2;

        public WeatherType currentForecastedWeatherType { get; private set; }

        public WeatherType SetForecastedWeatherTomorrow()
        {
            currentForecastedWeatherType = weatherProbabilities[0].ReturnLootFromTable<WeatherType>(weatherProbabilities);
            return currentForecastedWeatherType;
        }

        public ForecastRiddle GenerateRiddle(bool firstTime = false)
        {
            ForecastRiddle forecastRiddle = Instantiate(
                ResourceManager.instance.GameplayResources.Weathers.ForecastRiddle, this.transform);

            if (!firstTime)
                forecastRiddle.GenerateForecastRiddle(currentForecastedWeatherType, villagerSystem.villagers, villagerLimit);
            else
                forecastRiddle.GenerateFirstForecastRiddle(currentForecastedWeatherType, villagerSystem.villagers);

            return forecastRiddle;
        }
    }
}