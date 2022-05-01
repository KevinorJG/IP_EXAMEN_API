﻿using Common;
using Domain.Entities;
using Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Repository
{
    public class ApiGetRepository : IModel<City.Root>,IWeatherModel<Weather.Root>
    {
        protected string Url = String.Empty;
        protected string city = String.Empty;
        protected string lat = String.Empty;
        protected string lon = String.Empty;
        protected string dt = String.Empty;
        protected string icon = String.Empty;
        public static List<City.Root> cities = new List<City.Root>();

        public async Task<string> GetIcon()
        {
            //Expresion que obtiene la id del Icono de dicho pais
            cities.ForEach(x => icon =  x.weather[0].icon); 
            try
            {
                
                return await Task.FromResult($"{AppSettings.ApiIcons}{icon}{".png"}");
            }
            catch (IOException)
            {

                throw new NullReferenceException("No se puedo obtener la información de los iconos");
            }
        }

        public async Task<City.Root> GetWather()
        {
            try
            {
                using (WebClient web = new WebClient())
                {
                    Url = string.Format($"{AppSettings.ApiUrl}{city}&appid={ AppSettings.Token}&lang=es");
                    var Json = web.DownloadString(Url);
                    City.Root info = JsonConvert.DeserializeObject<City.Root>(Json);
                    cities.Add(info);
                    return await Task.FromResult(info);
                }
            }
            catch (IOException)
            {

                throw new NullReferenceException("No se puedo obtener la información");
            }
        }

        public async Task<Weather.Root> GetForecast()
        {
          //Expresiones que obtienen las coordenadas y  DateTime de dicho pais
            cities.ForEach(x => lat = x.coord.lat);
            cities.ForEach(x => lon = x.coord.lon);
            cities.ForEach(x => dt = x.dt);

            try
            {    
                using (WebClient web = new WebClient())
                {
                    Url = string.Format($"{AppSettings.ApiUrlOneCall}lat={lat}&lon={lon}&dt={dt}&appid={ AppSettings.Token}");
                    var Json = web.DownloadString(Url);                
                    var info = JsonConvert.DeserializeObject<Weather.Root>(Json);
                    return await Task.FromResult(info);
                }
            }
            catch (IOException)
            {

                throw new NullReferenceException("No se puedo obtener la información");
            }
        }

        public string Recibir(string city)
        {          
            return this.city = city;
        }
       
    }
}
