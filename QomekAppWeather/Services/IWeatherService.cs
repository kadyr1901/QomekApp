namespace QomekAppWeather.Services
{
    public interface IWeatherService
    {

        List<MinutelyWeather> GetWeatherList();
        MinutelyWeather Get(string id);
        void Remove(string id);
        MinutelyWeather Create(MinutelyWeather minutelyWeather);

    }
}
