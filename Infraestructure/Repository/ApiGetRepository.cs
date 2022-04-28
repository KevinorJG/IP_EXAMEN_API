using Common;
using Domain.Entities;
using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class ApiGetRepository : IModel<City.Root>,IWeatherModel<Weather.Root>
    {
        protected string Url = String.Empty;
        protected string city = String.Empty;
        protected string lon = String.Empty;
        protected string lat = String.Empty;
        protected string dt = String.Empty;

        public async Task<string> GetIcon()
        {
            try
            {

                return await Task.FromResult($"{AppSettings.ApiIcons}{GetWather(city).Result.weather[0].icon}{".png"}");
            }
            catch (IOException)
            {

                throw new NullReferenceException("No se puedo obtener la información de los iconos");
            }
        }

        public async Task<City.Root> GetWather(string city)
        {
            try
            {
                this.city = city;
                using (WebClient web = new WebClient())
                {
                    Url = string.Format($"{AppSettings.ApiUrl}{city}&appid={ AppSettings.Token}&lang=es");
                    var Json = web.DownloadString(Url);
                    City.Root info = JsonConvert.DeserializeObject<City.Root>(Json);
                    return await Task.FromResult(info);
                }
            }
            catch (IOException)
            {

                throw new NullReferenceException("No se puedo obtener la información");
            }
        }


        public async Task<City.Root> GetLocal_Location()
        {
            RegionInfo Country = new RegionInfo("NI");
            return await GetWather(Country.DisplayName);
        }
        public async Task<string> GetIconLocal()
        {
            return await GetIcon();
        }


        public Weather.Root GetForecast(string city)
        {
           
            lat = GetWather(city).Result.coord.lat;
            lon = GetWather(city).Result.coord.lon;
            dt = GetWather(city).Result.dt;
            try
            {
                
                using (WebClient web = new WebClient())
                {
                    Url = string.Format($"{AppSettings.ApiUrlOneCall}lat={lat}&lon={lon}&dt={dt}&appid={ AppSettings.Token}");
                    var Json = web.DownloadString(Url);
                    Weather.Root info = JsonConvert.DeserializeObject<Weather.Root>(Json);
                    return info;
                }
            }
            catch (IOException)
            {

                throw new NullReferenceException("No se puedo obtener la información");
            }
        }

       
    }
}
