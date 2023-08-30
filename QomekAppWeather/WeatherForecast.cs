using MongoDB.Bson.Serialization.Attributes;

namespace QomekAppWeather
{
    public class MinutelyWeather
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Offset")]
        public DateTimeOffset Dt { get; set; }
        [BsonElement("Precipitation")]
        public int Precipitation { get; set; }
    }

    public class WeatherData
    {
        // Other properties from the JSON structure

        public MinutelyWeather[] Minutely { get; set; }
    }
}