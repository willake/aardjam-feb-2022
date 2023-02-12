using UnityEngine;

namespace Game.Gameplay.Weathers
{
    public class WeatherFactory : MonoBehaviour
    {
        public Weather GenerateWeather(WeatherType weatherType, Transform parent = null)
        {
            Weather weather;
            switch (weatherType)
            {
                case WeatherType.Sunny:
                default:
                    weather = Instantiate(
                        ResourceManager.instance.GameplayResources.Weathers.Sunny,
                        parent
                    ).GetComponent<Weather>();
                    break;
                case WeatherType.Rainy:
                    weather = Instantiate(
                        ResourceManager.instance.GameplayResources.Weathers.Rainy,
                        parent
                    ).GetComponent<Weather>();
                    break;
            }

            return weather;
        }
    }
}