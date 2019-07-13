using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Refit;
using WeatherApp.Models;
using WeatherApp.Services;
using WeatherApp.ViewModels;
using Xamarin.Forms;

namespace WeatherApp.Pages
{
    public partial class HomePage : ContentPage
    {
        public HomePageModel VM { get; set; }
        public HomePage()
        {
            InitializeComponent();
            BindingContext = VM = new HomePageModel();
            WeatherListView.ItemsSource = Weathers;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //await CallApi();

        }
        public MainWeather Weather { get; set; } = new MainWeather { Name = "Test" };
        public ObservableCollection<MainWeather> Weathers { get; set; } = new ObservableCollection<MainWeather>();
        async Task CallApi(string cityName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(cityName))
                {
                    await this.DisplayAlert("Not Found", "Please enter a valid city", "Ok", "Cancel");
                    return;
                }

                cityName = cityName?.Trim();
                var nsAPI = RestService.For<IWeatherService>("https://api.openweathermap.org/data/2.5");
                Weather = await nsAPI.GetWeatherForCity(cityName);

                //Challenge use converters
                switch (Weather?.Weather[0]?.Main?.ToLower())
                {
                    case "rain":
                        Weather.Image = "Rainy";
                        break;
                    case "clear":
                        Weather.Image = "Clear";
                        break;
                    case "mist":
                        Weather.Image = "Misty";
                        break;
                    case "clouds":
                        Weather.Image = "Clouds";
                        break;
                    case "haze":
                        Weather.Image = "Haze";
                        break;
                    case "smoke":
                        Weather.Image = "Smoke";
                        break;
                    case "dust":
                        Weather.Image = "Dust";
                        break;
                    default:
                        Weather.Image = "Clear";
                        break;
                }
                Weathers.Insert(0, Weather);
            }
            catch (Exception ex)
            {
                await this.DisplayAlert("Not Found", ex.Message, "Ok", "Cancel");
            }
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            await CallApi(CityName.Text);
            CityName.Text = string.Empty;
        }
    }
}
