using Microsoft.Extensions.Options;
using MongoDB.Driver;
using QomekAppWeather.Services;

namespace QomekAppWeather
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.Configure<WeatherForecastSettings>(builder.Configuration.GetSection(nameof(WeatherForecastSettings)));
            builder.Services.AddSingleton<IWeatherForecastSettings>(sp => sp.GetRequiredService<IOptions<WeatherForecastSettings>>().Value);
            builder.Services.AddSingleton<IMongoClient>(x => new MongoClient(builder.Configuration.GetValue<string>("WeatherForecastSettings:ConnectionString")));
            builder.Services.AddScoped<IWeatherService, WeatherService>();
            builder.Services.AddHttpClient();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}