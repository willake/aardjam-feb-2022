using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public class WeatherRiddle : RiddleElement
    {
        private WeatherType forecastedWeatherType;
        public WeatherType predictedWeatherType;

        public WeatherRiddle(WeatherType _forecastedWeatherType, Villager _toldBy, StatementType _statementType, StatementCredibility _statementCredibility = StatementCredibility.Random) : base(_toldBy, _statementType, _statementCredibility)
        {
            forecastedWeatherType = _forecastedWeatherType;

            if (statementCredibility == StatementCredibility.Truth)
            {
                predictedWeatherType = _forecastedWeatherType;
            } else
            {
                predictedWeatherType = PickRandomWeatherType();
            }
        }

        private WeatherType PickRandomWeatherType()
        {
            var randomWeather = (WeatherType)Random.Range(0, 3);

            if (randomWeather != forecastedWeatherType)
            {
                return randomWeather;
            }
            else
                return PickRandomWeatherType();
        }
    }
}