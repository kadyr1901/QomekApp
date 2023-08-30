using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using QomekAppWeather.Services;

namespace QomekAppWeather.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherService weatherService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory,IWeatherService weatherService)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            this.weatherService = weatherService;

        }

        [HttpGet(Name = "FetchAndStoreWeather")]
        public async Task<IActionResult> FetchAndStoreWeather()
        {
            try
            {
                var apiKey = "ad0a884c0ccc9f356eb17253a9525eff";
                var apiUrl = $"https://api.openweathermap.org/data/3.0/onecall?lat=41&lon=74&exclude=hourly,daily,current,alerts&appid={apiKey}";

                var httpClient = _httpClientFactory.CreateClient();
                var response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    var weatherData = Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherData>(jsonResponse);

                    MinutelyWeather[] minutelyWeather = weatherData.Minutely;

                    // Store the weather data in the MongoDB database
                    weatherService.Create(minutelyWeather.FirstOrDefault());

                    return Ok("Weather data added to the database.");
                }

                return BadRequest("Failed to fetch weather data.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching and storing weather data.");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}