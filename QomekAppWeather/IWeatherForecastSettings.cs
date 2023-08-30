namespace QomekAppWeather
{
    public interface IWeatherForecastSettings
    {
        string WeatherCollectionName { get; set; }
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }
    }
}
