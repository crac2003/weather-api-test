namespace Weather.Service.OpenWeather
{
    public interface IOpenWeatherConfig
    {
        string OpenWeatherUrl { get; }
        string OpenWeatherKey { get; }
    }
}
