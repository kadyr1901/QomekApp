using MongoDB.Driver;

namespace QomekAppWeather.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IMongoCollection<MinutelyWeather> listOfWeather;

        public WeatherService(IWeatherForecastSettings settings, IMongoClient mongoClient)
        {
           var database= mongoClient.GetDatabase(settings.DatabaseName);
            listOfWeather=database.GetCollection<MinutelyWeather>(settings.WeatherCollectionName);
        }

        public MinutelyWeather Create(MinutelyWeather minutelyWeather)
        {
            listOfWeather.InsertOne(minutelyWeather);
            return minutelyWeather;
        }

        public MinutelyWeather Get(string id)
        {
            var weather= listOfWeather.Find(x => x.Id == id).FirstOrDefault();
            return weather;
        }

        public List<MinutelyWeather> GetWeatherList()
        {
            var weatherList = listOfWeather.Find(x => true).ToList();
            return weatherList;
        }

        public void Remove(string id)
        {
            listOfWeather.DeleteOne(x=>x.Id == id);
        }
    }
}
