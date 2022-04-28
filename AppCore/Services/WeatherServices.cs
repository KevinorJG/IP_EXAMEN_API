using AppCore.IServices;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCore.Services
{
    public class WeatherServices : IWeatherServices<Weather.Root>
    {
        private IWeatherModel<Weather.Root> model;

        public WeatherServices(IWeatherModel<Weather.Root> model)
        {
            this.model = model;
        }

        public Weather.Root GetForecast(string city)
        {
            return model.GetForecast(city);
        }
    }
}
