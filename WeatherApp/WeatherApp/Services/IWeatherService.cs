using System;
using System.Threading.Tasks;
using Refit;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public interface IWeatherService
    {
        [Get("/weather?q={citycont}&APPID=e005032acde39b596c2759df428e9196")]
        Task<MainWeather> GetWeatherForCity(string citycont);
    }
}
