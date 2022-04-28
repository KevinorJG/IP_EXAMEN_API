using AppCore.IServices;
using Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Globalization;
using Domain.Interfaces;
using Infraestructure.Repository;

namespace AppAPI
{
    public partial class Form1 : Form
    {

        private IServices<City.Root> services;
        private IWeatherServices<Weather.Root> weatherServices;

        //,container.Resolve<IServices<Weather.Root>>()

        //public Form1(IServices<City.Root> services, IServices<Weather.Root> services1)
        //{
        //    this.services = services;
        //    this.services1 = services1;

        //}


        public Form1(IServices<City.Root> services, IWeatherServices<Weather.Root> weatherServices)
        {
            this.weatherServices = weatherServices;
            this.services = services;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var root = services.GetLocal_Location();
            var getIcon = services.GetIconLocal(); 
            REST(root, getIcon);
            
        }

        public void REST(Task<City.Root> root, Task<string> getIcon)
        {
            Task.WaitAll(root, getIcon);

            pictureBox1.ImageLocation = getIcon.Result;
            labelCondicion.Text = root.Result.weather[0].main;
            labelDetalles.Text = root.Result.weather[0].description;
            labelViento.Text = root.Result.wind.speed + " m/s";
            labelPresion.Text = weatherServices.GetForecast(root.Result.name).hourly[0].pressure + "hPa";
            labelTem.Text = ((int)(double.Parse(root.Result.main.temp) - 273.15)).ToString() + "°C";
            labelCiudad.Text = root.Result.name + " / " + root.Result.sys.country;
            label7.Text = weatherServices.GetForecast(root.Result.name).lat; ;
            label9.Text = weatherServices.GetForecast(root.Result.name).lon;
            dynamic dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(double.Parse(weatherServices.GetForecast(root.Result.name).hourly[0].dt)).ToLocalTime();
            label11.Text = $"{dt}";

            
        }

        private void CityTexBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            

            if (e.KeyChar == (int)Keys.Enter)
            {
                try
                {
                    String city = CityTexBox.Text;
                   
                    Task<City.Root> clima = services.GetWather(city);
                    Task<string> icon = services.GetIcon();
                    if (clima.IsFaulted)
                    {
                        MessageBox.Show("Pais no existe","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    else
                    {
                        REST(clima, icon);
                    }

                }
                catch (Exception)
                {

                    throw ;
                }
            }
        }
    }
}
