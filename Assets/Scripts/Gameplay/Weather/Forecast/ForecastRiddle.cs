using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public class ForecastRiddle : MonoBehaviour
    {
        private WeatherType actualWeatherForecast;

        public WeatherRiddle weatherRiddle;
        public VillagerRiddle firstvillagerRiddle;
        public VillagerRiddle secondvillagerRiddle;

        public void GenerateFirstForecastRiddle(WeatherType actualWeatherForecast, List<Villager> villagers)
        {
            var forecastingVillager = villagers[0];
            weatherRiddle = new WeatherRiddle(actualWeatherForecast, forecastingVillager, RiddleElement.StatementType.AboutWeather, RiddleElement.StatementCredibility.Truth);

            Debug.Log(actualWeatherForecast);
            Debug.Log($"Weather forecaster: {weatherRiddle.toldBy.Name} {weatherRiddle.predictedWeatherType} {weatherRiddle.statementCredibility}");
        }

        public void GenerateForecastRiddle(WeatherType actualWeatherForecast, List<Villager> villagers, int limit = 2)
        {
            //Gets a shuffled list of villagers, takes first few
            var shuffledVillagers = RandShuffle<Villager>.Shuffle(villagers);

            var forecastingVillager = shuffledVillagers[0];
            weatherRiddle = new WeatherRiddle(actualWeatherForecast, forecastingVillager, RiddleElement.StatementType.AboutWeather);

            Debug.Log(actualWeatherForecast);
            Debug.Log($"Weather forecaster: {weatherRiddle.toldBy.Name} {weatherRiddle.predictedWeatherType} {weatherRiddle.statementCredibility}");

            if (shuffledVillagers.Count == 1) return;

            var aboutWeatherVillager = shuffledVillagers[1];
            firstvillagerRiddle = new VillagerRiddle(weatherRiddle, aboutWeatherVillager, RiddleElement.StatementType.AboutVillager);

            Debug.Log($"Talking person: {firstvillagerRiddle.toldBy.Name} {firstvillagerRiddle.villagerRiddleBelief} {firstvillagerRiddle.statementCredibility}");

            if (shuffledVillagers.Count == 2) return;

            var aboutSecondVillager = shuffledVillagers[2];
            secondvillagerRiddle = new VillagerRiddle(firstvillagerRiddle, aboutSecondVillager, RiddleElement.StatementType.AboutVillager);

            Debug.Log($"Second Talking person: {secondvillagerRiddle.toldBy.Name} {secondvillagerRiddle.villagerRiddleBelief} {secondvillagerRiddle.statementCredibility}");
        }

        public string GenerateWeatherString()
        {
            var randValue = Random.value;

            if (randValue <= 0.33f)
                return $"Tomorrow, the weather is going to be {weatherRiddle.predictedWeatherType}";
            else if (randValue >= 0.66f)
                return $"I feel it in my cloth! Tomorrow it will be {weatherRiddle.predictedWeatherType}";
            else
                return $"Hmm.. It sure is feeling {weatherRiddle.predictedWeatherType}, isn't it?";
        }
    }
}