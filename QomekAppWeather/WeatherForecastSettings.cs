﻿namespace QomekAppWeather
{
    public class WeatherForecastSettings : IWeatherForecastSettings
    {
        public string WeatherCollectionName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
    }
}
